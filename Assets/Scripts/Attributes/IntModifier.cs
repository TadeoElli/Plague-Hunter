using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IntModifier : IModifier<int>
{

    [SerializeField] public int value;
    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public int CreateEmpty()
    {
        return 0;
    }

    public int GetValue()
    {
        Debug.Log($"value to add {value}");
        return value;
    }
}
