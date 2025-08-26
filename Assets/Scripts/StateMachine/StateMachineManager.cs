using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    protected IState currentState;

    public void ChangeState(IState next)
    {
        if (currentState == next) return;
        currentState?.OnExit();
        currentState = next;
        currentState?.OnEnter();
    }
    public void Update()
    {
        currentState?.OnUpdate();
    }
}
