using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    [SerializeField] GameObject explosion;
    Damageable dmg;

    float timer;

    private void Awake()
    {
        dmg = GetComponent<Damageable>();
        dmg.setDeathCallback(DeathCallback);
    }

    void DeathCallback(Damageable dmg)
    {
        var barrelExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    

}
