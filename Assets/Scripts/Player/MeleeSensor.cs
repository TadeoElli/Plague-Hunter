using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//TPFinal - Tadeo Elli.

public class MeleeSensor : MonoBehaviour
{

    Collider myCol;
    Renderer myRendeer;
    Action<Damageable> damageCallback;

    private void Start()
    {
        myCol = this.gameObject.GetComponent<Collider>();
        myRendeer = this.gameObject.GetComponent<Renderer>();
        this.TurnOff();
    }
    public void setDealDamageCallback(Action<Damageable> callback)
    {
        damageCallback = callback;
    }

    public void TurnOn()
    {
        myCol.enabled = true;
        //myRendeer.enabled = true;
    }
    public void TurnOff()
    {
        myCol.enabled = false;
        //myRendeer.enabled = false;
    }

    public void Attack(float duration)
    {
        TurnOn();
        Invoke("TurnOff",duration);
    }
    private void OnTriggerEnter(Collider other)
    {
        Damageable dmg = other.GetComponent<Damageable>();
        if (dmg != null)
        {
              DealDamage(dmg);  
        }
    }
    
    void DealDamage(Damageable dmg)
    {
        damageCallback.Invoke(dmg);
    }
}
