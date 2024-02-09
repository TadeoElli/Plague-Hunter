using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called before the first frame update

    private float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DealDamage(Damageable dmg)
    {
        damage = 0;

        dmg.TakeDamage(damage, DamageTypes.basic);
    }
}
