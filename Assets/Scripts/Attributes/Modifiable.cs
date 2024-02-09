using System.Collections.Generic;
using UnityEngine;
public delegate void ModifiedEvent();

[System.Serializable]
public class Modifiable<T, W> where T :  class, IModifier<W>, new()
{
    //[SerializeField] private IModifier StartValue;
    [SerializeField] private W _value;
    public W Value { get { return _value; } private set { _value = value; } }

    private List<T> modifiers = new List<T>();

    public event ModifiedEvent ValueModifiedEvent;
    public Modifiable(ModifiedEvent method = null)
    {
        //Debug.Log("ModifiableInt created");
        UpdateModifiedValue();
        if (method != null)
            ValueModifiedEvent += method;
        //if (baseModifier != null)
        //    AddModifier(baseModifier);
        //Debug.Log(string.Join(", ", modifiers));
    }

    public void RegisterModEvent(ModifiedEvent method)
    {
        ValueModifiedEvent += method;
    }
    public void UnregisterModEvent(ModifiedEvent method)
    {
        ValueModifiedEvent -= method;
    }

    public void UpdateModifiedValue()
    {
        T test = new T(); //TODO: esto se crea porque no se puede crear un metodo static en una interfaz
        W newValueSum = test.CreateEmpty();
        foreach (T mod in modifiers)
        {
            mod.AddValue(ref newValueSum);
            //newValueSum += mod.GetValue();
            //Debug.Log("value to add " + newValueSum);
        }
        Value = newValueSum;
        if (ValueModifiedEvent != null)
            ValueModifiedEvent.Invoke();
    }

    public void AddModifier(T mod)
    {
        modifiers.Add(mod);
        UpdateModifiedValue();
    }
    public void RemoveModifier(T mod)
    {
        modifiers.Remove(mod);
        UpdateModifiedValue();
    }
}