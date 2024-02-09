using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHability : OilEffect
{
    public AnimEvents events;
    PlayerCombat combat;

    [SerializeField] GameObject fire;
    [SerializeField] GameObject fireAttack;
    [SerializeField] float time;
    [SerializeField] float timer;
    bool active = false;

    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
    }
    void Start()
    {
        events.ADD_EVENT("fire_attack_begin", FireTrail_begin);
        events.ADD_EVENT("fire_attack_end", FireTrail_end);
    }

    public override void StartEffect()
    {
        fire.SetActive(true);
        active = true;
    }

    public override void EndEffect()
    {
        fire.SetActive(false);
        fireAttack.SetActive(false);
        active = false;
    }


    /*
        public bool FirePotionUpdate()
        {
            if (timer >= time)
            {
                EndEffect();
                timer = 0;
            }
            if (active == true)
            {
                timer = timer + 1 * Time.deltaTime;
            }
            return active;
        }
    */

    void FireTrail_begin()
    {
        if (active == true)
        {
            fireAttack.SetActive(true);
        }
    }

    void FireTrail_end()
    {
        if (active == true)
        {
            fireAttack.SetActive(false);
        }
    }

}
