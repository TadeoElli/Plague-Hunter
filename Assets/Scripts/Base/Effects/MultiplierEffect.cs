using UnityEngine;

public abstract class MultiplierEffect : Effect
{
    //effectos de duracion finita e infinita. podria ser un boost de velocidad, inmortalidad o cosas que duren un tiempo
    float duration; // if 0, es infinito

    protected MultiplierEffect(Effectable effectable, EffectTypes type, float duration) : base(effectable, type)
    {
    }
}
