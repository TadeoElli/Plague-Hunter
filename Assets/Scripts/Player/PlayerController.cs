//using System.Reflection.Metadata;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
//TPFinal - Tadeo Elli.

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    Rigidbody myRig;
    private bool prev = false;
    public bool isStealth = false;
    public bool hasCrit = false;
    public Vector3 lastPosition;

    [SerializeField] Transform root;
    //[SerializeField] Animator anim;
    public AnimEvents events;
    private PlayerView view;
    private PlayerMovement move;
    private PlayerCombat combat;
    private PlayerLookAt lookAt;
    private PlayerInGameInventory inventory;
    private Collider myCol;
    public PotionPreview preview;
    public PotionRangePreview rangePreview;
    public GameObject numberUI;

    private EntityOnFrozen onFrozen;

    public float x, y; //localiza la posicion de nuetro personaje
    public float stealthTime, timer;
    Damageable dmg;


    private enum State
    {
        Iddle,
        DefaultMovement,
        BlockMovement,
        Dodging,
        Frozen,
        Death,
        Drinking,
        DoingAction,
        Throwing,
        //OnPauseMenu,
        OnInventoryMenu
        /*
        Attacking,
        Throwing
        Jump,
        TakeDamage,
        Drink,
        Recharge,
        Enchant,
        Mutate
        */
    }
    private State state = State.Iddle;

    private Vector3 input;
    private Vector3 dodgeDir;



    void Awake()
    {
        dmg = GetComponent<Damageable>();
        dmg.numberUI = numberUI;

        dmg.setDeathCallback(DeathCallback);
        dmg.setOnTakeDamageCallback(TakeDamageCallback);
    }
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        combat = GetComponent<PlayerCombat>();
        view = GetComponent<PlayerView>();
        move = GetComponent<PlayerMovement>();
        lookAt = GetComponentInChildren<PlayerLookAt>();
        myCol = GetComponent<Collider>();
        inventory = GetComponent<PlayerInGameInventory>();
        onFrozen = GetComponentInChildren<EntityOnFrozen>();


        //TODO: corregir nombres.
        events.ADD_EVENT("enable_motion", EndAction);
        events.ADD_EVENT("end_throwing", EndAction);
        events.ADD_EVENT("dodge_end", EndAction);
        events.ADD_EVENT("throw_end", StopThrowing);
        events.ADD_EVENT("death_anim", RealDeath);
        events.ADD_EVENT("start_healing", Heal);
        events.ADD_EVENT("end_healing", EndHealing);
        events.ADD_EVENT("throw_stop", WaitThrowing);


    }
    private void Update()
    {
        if (!GameManager.isPaused)
        {
            Machinery();
        }
    }

    private void Machinery()
    {

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        input = new Vector3(x, 0, y);

        if(timer > stealthTime && isStealth)
        {
            view.FinishStealth();
            FinishStealth();
        }
        else
        {
            timer = timer + 1f * Time.deltaTime;
        }
        //Debug.Log(input);

        if (!move.isMovementEnabled())
        {
            state = State.BlockMovement;

        }
        //Debug.Log(state);
        switch (state)
        {
            case State.DefaultMovement:
                if (isIddle())
                {
                    state = State.Iddle;
                    GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");
                }
                else if (Input.GetButtonDown("Dodge"))
                {
                    GameManager.GetAudioManager().PlayClipByName("PlayerRoll");

                    view.Dodge();
                    StartDodging();
                    state = State.Dodging;
                }
                CheckInputs();
                DefaultMovementState();
                break;

            case State.Iddle:
                if (!isIddle())
                {
                    state = State.DefaultMovement;
                    GameManager.GetAudioManager().PlayClipByName("PlayerWalkingFootsteps");

                }
                CheckInputs();
                IddleState();
                break;

            case State.BlockMovement:
                if (move.isMovementEnabled())
                {
                    state = State.DefaultMovement;
                }
                BlockMovementState();
                break;

            case State.Throwing:
                if (Input.GetButtonDown("Fire1") && preview.IsInRange() && prev)
                {
                    prev = false;
                    preview.DesactivatePotionPreview();
                    rangePreview.DesactivatePotionPreview();
                    view.StartAnimation();
                    inventory.UseSelectedPotion();
                    isStealth = false;
                    view.FinishStealth();
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    preview.DesactivatePotionPreview();
                    rangePreview.DesactivatePotionPreview();
                    //inventory.ReturnSelectedPotion();
                    view.CancelThrow();
                    state = State.DefaultMovement;
                }
                else
                {
                    DoingActionState();
                    preview.ShowPotionFeedback();
                }
                break;

            case State.Frozen:
                FrozenState();
                break;

            case State.OnInventoryMenu:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    UIManager.ToggleInventory();
                    state = State.DefaultMovement;
                }
                InvetoryState();
                break;
            /*
            case State.OnPauseMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UIManager.TogglePauseGame();
                    state = State.DefaultMovement;
                }
                BlockMovementState();
                break;
            */
            case State.Drinking: DoingActionState(); break;
            case State.Dodging: DodgingState(); break;
            case State.DoingAction: DoingActionState(); break;
            case State.Death: DeathState(); break;

            default:
                state = State.Iddle;
                break;
        }

    }

    private void InvetoryState()
    {
        BlockMovementState();
        view.SetAnimationDir(Vector3.zero);
    }

    private bool isIddle()
    {
        if (input == Vector3.zero)
        {
            //Debug.Log("player is iddle");
            return true;
        }
        //Debug.Log("player is moving");
        return false;
    }
    private void StartDodging()
    {
        dodgeDir = input;
    }

    private void IddleState()
    {
        move.StopMoving();
        lookAt.enableLookAtMouse = true;
    }

    private void StartThrowing()
    {
        combat.Throw(inventory.GetSelectedPotion());
        
    }

    private void WaitThrowing()
    {
        view.PauseAnimation();
        preview.ActivatePotionPreview();
        rangePreview.ActivatePotionPreview();
        GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");
        prev = true;
    }
    private void StopThrowing()
    {
        view.CancelThrow();
        EndAction();
    }
    private void StartDrinking()
    {
        view.Drink();
    }
    private void StartAttacking()
    {
        combat.BasicAttack();
    }
    private void StartHeavyAttacking()
    {
        combat.HeavyAttack();
    }
    private void DodgingState()
    {
        GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");
        lookAt.enableLookAtMouse = false;
        lookAt.SetLookAt(dodgeDir);
        view.SetAnimationDir(lookAt.LookingAt());
        move.Dash(dodgeDir);
    }

    private void DoingActionState()
    {
        BlockMovementState();
    }
    private void DefaultMovementState()
    {
        view.StartAnimation();
        move.EnableMovement();
        lookAt.enableLookAtMouse = true;
        Vector3 dirview = root.InverseTransformDirection(input);
        view.SetAnimationDir(dirview); //no aplica cuando esta dodgeando
        move.Walk(input);
    }
    private void DeathState()
    {
        BlockMovementState();

    }
    private void BlockMovementState()
    {
        move.StopMoving();
        GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");
        if (state != State.Throwing)
        {
            lookAt.enableLookAtMouse = false;
        }
        else
        {
            lookAt.enableLookAtMouse = true;
        }
    }

    private void FrozenState()
    {
        BlockMovementState();
        view.PauseAnimation();
    }

    public void EndAction() => state = State.DefaultMovement;
    public void TurnIceOn()
    {
        state = State.Frozen;
    }

    public void TurnIceOff()
    {
        if (state != State.Death)
        {
            state = State.DefaultMovement;
        }

    }
    public void EndHealing() => view.StopHealing();

    public void Heal()
    {
        
        //inventory.UseHealingPotion();
        Item var = inventory.UseHealingPotion();
        switch(var.name)
        {
            case("HealthPotion"):
                float healAmount = 30f;
                if (dmg.life > dmg.maxLife - healAmount)
                {
                    healAmount = dmg.maxLife - dmg.life;
                }
                dmg.life = dmg.life + healAmount;
                var invokeNumber = Instantiate(numberUI, transform.position + 0.5f * Random.onUnitSphere, transform.rotation);
                invokeNumber.GetComponent<DamageNumbers>().HealNumbers(healAmount);
                state = State.DefaultMovement;
                break;
            case("StealthPotion"):
                Debug.Log("Stealth");
                view.Stealth();
                state = State.DefaultMovement;
                isStealth = true;
                lastPosition = this.transform.position;
                timer = 0f;
                hasCrit = true;
                break;
            default:
                break;
        }

    }
    void FinishStealth()
    {
        isStealth = false;
    }

    void DeathCallback(Damageable dmg)
    {
        state = State.Death;
        onFrozen.EndEffect();
        view.StartAnimation();
        Debug.Log("El player a muerto");
        GameManager.GetAudioManager().PlayClipByName("PlayerDeath");
        view.Death();
        //myRig.useGravity = false;
        //myCol.enabled = false;
        move.BlockMovement();
        //Destroy(dmg);
    }
    void TakeDamageCallback(Damageable dmg)
    {
        if (state == State.Drinking)
        {
            view.StopHealing();
            state = State.DefaultMovement;
        }
        //Debug.Log("El player recibio daño");
        isStealth = false;
        view.FinishStealth();
        view.ReceiveDamage(dmg);
        GameManager.GetAudioManager().PlayClipByName("PlayerRecieveDamage");
    }

    public void RealDeath()
    {
        GameManager.EndScene();
        //Destroy(this.gameObject);
    }
    public void CheckInputs()
    {

        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.TogglePauseGame();
            state = State.OnPauseMenu;
        }*/
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            view.SetAnimationDir(Vector3.zero);
            UIManager.ToggleInventory();
            state = State.OnInventoryMenu;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            StartAttacking();
            state = State.DoingAction;
        }
        else if (Input.GetButtonDown("Fire2") && state != State.Throwing)
        {
            StartHeavyAttacking();
            state = State.DoingAction;
        }
        else if (Input.GetButtonDown("Drink") && inventory.HasHealingPotion())
        {
            StartDrinking();
            GameManager.GetAudioManager().PlayClipByName("Drinking");
            state = State.Drinking;
        }
        else if (Input.GetButtonDown("Fire3") && inventory.HasSelectedThrowable())
        {
            StartThrowing();
            //prev = true;
            state = State.Throwing;
        }


        else if (Input.GetKeyDown(KeyCode.G))
        {
            AttemptToOil(KeyCode.G);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            AttemptToOil(KeyCode.F);
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            inventory.SwitchPotionUP();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)    //forward
        {
            inventory.SwitchPotionUP();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)    //backward
        {
            inventory.SwitchPotionDown();
        }
    }

    void AttemptToOil(KeyCode key)
    {
        if (inventory.HasSelectedOil(key) && !combat.isWeaponUsingOil)
        {
            combat.SetSwordOnEffect(inventory.UseSelectedOil().oilType);
            //TODO: add oiling animation
        }
    }
}
