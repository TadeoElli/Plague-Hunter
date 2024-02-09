using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OilEffectsType
{
    fire,
    ice
}

public abstract class OilEffect : MonoBehaviour
{
    [SerializeField] public OilEffectsType oilType;
    public abstract void StartEffect();
    public abstract void EndEffect();
}