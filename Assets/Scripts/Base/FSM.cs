using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.

public class FSM<T>
{
    IState _currentState;

    Dictionary<T, IState> _allStates = new Dictionary<T, IState>();

    public void AddState(T key, IState value)
    {
        if (!_allStates.ContainsKey(key)) _allStates.Add(key, value);
        else _allStates[key] = value;
    }

    public void ChangeState(T nextState)
    {
        if (_currentState != null) _currentState.OnExit();
        _currentState = _allStates[nextState];
        _currentState.OnEnter();
    }

    public void Update()
    {
        _currentState.OnUpdate();
    }

    public void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }
}
