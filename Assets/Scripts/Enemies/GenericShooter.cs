using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericShooter : MonoBehaviour
{
    public Action callback = delegate { };

    float timer;
    [SerializeField] float time_to_pulse = 1;

    public void Configure(Action _callback)
    {
        callback = _callback;
    }

    bool anim;
    public void Play()
    {
        anim = true;
    }
    public void Stop()
    {
        anim = false;
    }

    private void Update()
    {
        if (!anim) return;

        if (timer < time_to_pulse)
        {
            timer = timer + 1 * Time.deltaTime;
        }
        else
        {
            callback.Invoke();
            //pulso
            timer = 0;
        }
    }
}
