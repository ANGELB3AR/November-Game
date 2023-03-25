using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class StateMachine : NetworkBehaviour
{
    State currentState;

    void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
