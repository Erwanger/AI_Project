using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private Dictionary<Type, BaseState> availableStates;

    public BaseState currentState;
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == null)
        {
            currentState = availableStates.Values.First();
        }

        Type nextState = currentState.Tick();

        if(nextState != null && nextState != currentState.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        currentState = availableStates[nextState];
        OnStateChanged?.Invoke(currentState);
    }

    public void ChangeState(Type state)
    {
        currentState = availableStates[state];
        OnStateChanged?.Invoke(currentState);
    }
}
