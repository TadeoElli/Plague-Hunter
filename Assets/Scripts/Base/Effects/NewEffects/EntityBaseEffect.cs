using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBaseEffect: MonoBehaviour
{
    public abstract void ApplyEffect();
    public abstract void EndEffect();
    public abstract void SetDamagable(Damageable dmg);
    
}