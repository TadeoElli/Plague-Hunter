using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionDamage : MonoBehaviour
{

    [SerializeField] float explosiveDamage;
    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.gameObject.name} explota contra ${other.gameObject.name}");
        Damageable dmg = other.GetComponentInChildren<Damageable>();
        if (dmg != null)
        {
            //Debug.Log("BOOM!!");
            DealDamage(dmg);

        }
    }
    void DealDamage(Damageable dmg)
    {
        explosiveDamage = (int)Random.Range(10f, 21f);
        dmg.TakeDamage(explosiveDamage, DamageTypes.explosion);
    }

}
