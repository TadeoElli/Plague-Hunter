using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOnIce : EntityBaseEffect
{
    Damageable damageable;

    [SerializeField] float timeOnFrozen = 5;
    [SerializeField] public bool isOnFrozen = false;
    public float slow = 1;
    [SerializeField] GameObject ice;

    public override void SetDamagable(Damageable dmg)
    {
        damageable = dmg;
    }

    public override void ApplyEffect()
    {
        SetOnFrozen();
    }

    public override void EndEffect()
    {
        DisableFrozen();
    }

    public void SetOnFrozen()
    {
        EnableFrozen();
        Invoke("DisableFrozen", timeOnFrozen);
    }
    void EnableFrozen()
    {
        isOnFrozen = true;
        ice.SetActive(true);


    }
    void DisableFrozen()
    {
        isOnFrozen = false;
        ice.SetActive(false);
    }

}
