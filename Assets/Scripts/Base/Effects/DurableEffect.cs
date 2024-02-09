using UnityEngine;

public abstract class DurableEffect : Effect
{
    //effectos de duracion finita e infinita. podria ser un boost de velocidad, inmortalidad o cosas que duren un tiempo
    float duration; // if 0, es infinito

    protected DurableEffect(Effectable effectable, EffectTypes type) : base(effectable, type)
    {
    }
}
