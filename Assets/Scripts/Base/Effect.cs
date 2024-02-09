using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    Effectable afected;
    EffectTypes effectType;
    public abstract void ApplyEffect(float multiplier);
    public abstract void OnEffectEnd(float multiplier);
    public abstract EffectTypes GetEffectTypes();
    
    public Effect(Effectable effectable, EffectTypes type){
        afected = effectable;
        effectType = type;
    }
    
    //Visual Things
    public abstract void Visualize();
    public abstract void EndVisualize();
    public abstract void StartVisualize();

    
}