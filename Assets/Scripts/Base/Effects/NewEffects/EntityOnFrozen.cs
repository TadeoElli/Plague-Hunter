using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOnFrozen : EntityBaseEffect
{

    [SerializeField] float tangledTime = 65f;
    float frozenTime;
    public float timeLeft => frozenTime;
    KeyCode lastFrozenKeyPressed = KeyCode.B;
    bool isFrozen = false;
    PlayerController entity; //TODO: eventualmente cambiar de playercontroller a una clase abstracta que puedan usar otras entidades
    [SerializeField] Renderer iceRender;
    [SerializeField] GameObject tangledCanvas;

    private void Start()
    {
        entity = GameManager.GetPlayer();
        //tangledCanvas = FindObjectOfType<TangledCanvas>();
    }

    private void Update()
    {
        if (isFrozen)
        {
            //Debug.Log($"Why am i here?: time: {frozenTime}");
            if (frozenTime <= 0)
            {
                //Debug.Log("Why am i here?");
                EndEffect();
            }
            FrozenState();
        }
    }

    Damageable damageable;
    public override void SetDamagable(Damageable dmg)
    {
        damageable = dmg;
    }


    public override void ApplyEffect()
    {
        isFrozen = true;
        frozenTime = tangledTime;
        lastFrozenKeyPressed = KeyCode.B;
        Debug.Log("apply frozen");
        entity.TurnIceOn();
        GameManager.GetAudioManager().PlayClipByName("TrapFreeze");
        iceRender.enabled = true;
        TurnOnUI();
        //Debug.Log("apply frozen");
    }

    void TurnOnUI()
    {
        tangledCanvas.gameObject.SetActive(true);
        tangledCanvas.GetComponent<TangledCanvas>().SetOn(tangledTime);
    }
    void TurnOffUI()
    {
        //tangledCanvas.SetOff();
        tangledCanvas.gameObject.SetActive(false);
    }
    public override void EndEffect()
    {
        isFrozen = false;
        Debug.Log("end frozen");
        entity.TurnIceOff();
        iceRender.enabled = false;
        TurnOffUI();
    }
    private void FrozenState()
    {
        frozenTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && lastFrozenKeyPressed != KeyCode.A)
        {
            frozenTime = frozenTime - 5f;
            lastFrozenKeyPressed = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.D) && lastFrozenKeyPressed != KeyCode.D)
        {
            frozenTime = frozenTime - 5f;
            lastFrozenKeyPressed = KeyCode.D;
        }
    }

}
