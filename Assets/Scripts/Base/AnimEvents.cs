using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//TPFinal - Tadeo Elli.
public class AnimEvents : MonoBehaviour
{
    Dictionary<string, Action> events = new Dictionary<string, Action>();

    public void ADD_EVENT(string eventname, Action callback)
    {
        if (!events.ContainsKey(eventname))
        {
            events.Add(eventname, callback);
        }
        else
        {
            throw new Exception("no podes hacer esto pa!");
        }
    }

    public void ANIM_EVENT(string parameters)
    {
        events[parameters].Invoke();
    }
    
}
