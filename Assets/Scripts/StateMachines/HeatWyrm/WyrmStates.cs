using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WyrmStates
{
    protected Wyrm wyrm;
    protected WyrmFinalState fsm;

    public WyrmStates(Wyrm context, WyrmFinalState FSM)
    {
        wyrm = context;
        fsm = FSM;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public abstract IEnumerator OnMessage(Message msg);

    protected void ChangeState(WyrmStates newState)
    {
        Exit();
        newState.Enter();
        wyrm.currentState = newState;
    }
}
