using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier<T>
{
    void AddValue(ref T baseValue);
    T GetValue();

    T CreateEmpty(); //TODO: ver si esto eventualmente se puede hace static

}

