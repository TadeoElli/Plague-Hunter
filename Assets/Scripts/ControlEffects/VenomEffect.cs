using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VenomEffect : TickedEffect
{
    public float cooldown = 1f;
    public float damage = 3f;
    private float totalTime = 0;

    public VenomEffect(Effectable effectable, EffectTypes type) : base(effectable, type)
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyEffect(float multiplier)
    {
        throw new System.NotImplementedException();
    }

    public override void EndVisualize()
    {
        throw new System.NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override EffectTypes GetEffectTypes()
    {
        throw new System.NotImplementedException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void OnEffectEnd(float multiplier)
    {
        throw new System.NotImplementedException();
    }

    public override void StartVisualize()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Visualize()
    {
        throw new System.NotImplementedException();
    }

    protected void ApplyEffect(Damageable dmg)
    {
        totalTime += Time.deltaTime;
        if (totalTime >= cooldown)
        {
            //Damageable dmg = other.GetComponent<Damageable>();
            damage = (int) Random.Range(2f,5f);
            dmg.TakeDamage(damage, DamageTypes.venom);
            totalTime = 0;
        }
    }
}
