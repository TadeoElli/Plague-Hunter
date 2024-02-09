using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOnFire : EntityBaseEffect
{
    [SerializeField] GameObject fire;

    float timeOnFire = 3;
    float onFireDamageCooldown = 0.25f;
    float fireTimeCounter = 0;
    bool isOnFire = false;
    float fireDamage = 2f;
    Damageable damageable;

    public override void SetDamagable(Damageable dmg){
        damageable = dmg;
    }

    public override void ApplyEffect()
    {
        SetOnFire();
    }

    public override void EndEffect()
    {
        DisableFire();
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (isOnFire == true)
        {
            ApplyFireDamage();
        }

    }
    public void ApplyFireDamage()
    {
        if(this.gameObject != null)
        {
            fireTimeCounter += Time.deltaTime;
            if (fireTimeCounter >= onFireDamageCooldown)
            {
                //Damageable dmg = other.GetComponent<Damageable>();
                fireDamage = (int)Random.Range(2f, 5f);
                damageable.TakeDamage(fireDamage, DamageTypes.fire);
                fireTimeCounter = 0;
            }
        }
    }

    void SetOnFire()
    {
        EnableFire();
        Invoke("DisableFire", timeOnFire);
    }

    void EnableFire()
    {
        isOnFire = true;
        fire.SetActive(true);
        //var fireDamage = Instantiate(fire, enemy.transform.position, enemy.transform.rotation);
    }

    void DisableFire()
    {
        isOnFire = false;
        fire.SetActive(false);
    }

}
