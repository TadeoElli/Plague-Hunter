using UnityEngine;

public abstract class TickedEffect : DurableEffect
{
    // veneno, fuego, curacion progresiva. (aplican cada x ticks)
    float tickTime;

    protected TickedEffect(Effectable effectable, EffectTypes type) : base(effectable, type)
    {
    }
}
