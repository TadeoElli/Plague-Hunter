using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.

public interface IState
{
    void OnEnter();

    void OnUpdate();

    void OnFixedUpdate();

    void OnExit();
}

