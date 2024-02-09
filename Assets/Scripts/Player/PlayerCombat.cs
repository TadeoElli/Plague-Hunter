using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

//TPFinal - Tadeo Elli.

public class PlayerCombat : MonoBehaviour
{
    //Visuals
    [SerializeField] AnimEvents events;
    private PlayerController player;
    private PlayerView view;

    //Throwing
    [SerializeField] GameObject throwPoint;
    private Throwable potion;
    private Transform target;

    private FireHability fireHability;
    [SerializeField] private float fireTimer;
    private IceHability iceHability;

    private bool oilInUse = false;
    public bool isWeaponUsingOil => oilInUse;

    //Melee
    MeleeSensor meleeSensor;
    //[SerializeField][Range(5f, 15f)] float basicDamage;
    //[SerializeField][Range(8f, 18f)] float heavyDamage;
    bool isHeavyAttack;
    public bool weapon_on_ice = false; //TODO: hacer refactor del prender fuego
    public bool weapon_on_fire = false; //TODO: hacer refactor del prender hielo


    private void Start()
    {
        view = GetComponent<PlayerView>();
        player = GetComponent<PlayerController>();
        meleeSensor = gameObject.GetComponentInChildren<MeleeSensor>();

        fireHability = gameObject.GetComponentInChildren<FireHability>();
        iceHability = gameObject.GetComponentInChildren<IceHability>();

        PotionPreview potionPreview = GameManager.GetPotionPreview();
        target = potionPreview.transform;

        events.ADD_EVENT("melee_attack_begin", RealBegin_Melee);
        events.ADD_EVENT("melee_attack_end", RealEnd_Melee);
        events.ADD_EVENT("throw_begin", RealThrow);


        meleeSensor.setDealDamageCallback(OnDealDamage);
        //mySensorMelee.TurnOff();
    }

    public void SetSwordOnEffect(OilEffectsType type)
    {
        switch (type)
        {
            case OilEffectsType.fire:
                SetSwordOnFire();
                break;
            case OilEffectsType.ice:
                SetSwordOnIce();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        oilInUse = weapon_on_fire || weapon_on_ice;
    }

    #region Real events

    void RealBegin_Melee()
    {
        meleeSensor.TurnOn();
        //Debug.Log("player attacks");
    }
    void RealEnd_Melee()
    {
        meleeSensor.TurnOff();
        player.isStealth = false;
        view.FinishStealth();
    }
    void RealThrow()
    {
        var p = GameObject.Instantiate(potion);
        //p.Move(throwPoint.transform.position, my_root.forward + my_root.up / 3);
        p.Throw(throwPoint.transform.position, target.position);
    }
    #endregion

    void OnDealDamage(Damageable dmg)
    {
        int actualDamage = 0; //TODO: esto tiene que ser float en realidad (revisar). esta con int para que el feedback de dmg sea con enteros
        if (isHeavyAttack)
        {
            actualDamage = Random.Range(8, 16);
            if(player.isStealth) actualDamage = actualDamage * 2;

            //heavyDamage = Random.Range(8f, 16f);
        }
        else
        {
            //basicDamage = Random.Range(5f, 10f);
            actualDamage = Random.Range(5, 10);
            if(player.isStealth) actualDamage = actualDamage * 2;
        }

        if (weapon_on_fire)
        {
            actualDamage += 5;
            dmg.TakeDamage(actualDamage, DamageTypes.fire);
            dmg.SetOnFire();
            //Debug.Log("bicho on fire");
        }
        /*
        else if (weapon_on_ice)
        {
            actualDamage += 2;
            dmg.TakeDamage(actualDamage, DamageTypes.fire);
            dmg.SetOnFrozen();
            //Debug.Log("bicho congelado");
        }
        */
        else
        {
            if(player.isStealth)
            {
                dmg.TakeDamage(actualDamage, DamageTypes.critical);
            }
            else
            {
                dmg.TakeDamage(actualDamage, DamageTypes.basic);
            }
            
            //Debug.Log("bicho dañado");
        }
    }

    public void BasicAttack()
    {
        isHeavyAttack = false;
        view.Attack();
        GameManager.GetAudioManager().PlayClipByName("PlayerFastSlash");
        GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");
    }

    public void SetSwordOnFire()
    {
        GameManager.GetAudioManager().PlayClipByName("FireThreeSeconds");
        weapon_on_fire = true;
        fireHability.StartEffect();
        Invoke("UnSetSwordOnFire", fireTimer);
    }
    public void UnSetSwordOnFire()
    {
        weapon_on_fire = false;
        fireHability.EndEffect();
    }
    public void SetSwordOnIce()
    {

    }
    public void UnSetSwordOnIce()
    {

    }
    public void HeavyAttack()
    {
        isHeavyAttack = true;
        view.HeavyAttack();
        GameManager.GetAudioManager().PlayClipByName("PlayerStrongSlash");
        GameManager.GetAudioManager().StopClipByName("PlayerWalkingFootsteps");

    }

    public void Throw(Throwable throwable)
    {
        potion = throwable;
        view.ThrowPotion();
        //RealThrow();
    }

}