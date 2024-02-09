using UnityEngine;

public abstract class InstantEffect : Effect
{
    //Ejemplos, explosion, curacion instantanea, o cancelado de efectos
    protected InstantEffect(Effectable effectable, EffectTypes type) : base(effectable, type)
    {
    }
}
