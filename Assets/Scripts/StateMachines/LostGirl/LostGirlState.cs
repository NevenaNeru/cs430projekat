using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LostGirlState
{
    protected LostGirl girl;
    protected LostGirlFinalStateMachine fsm;

    public LostGirlState(LostGirl context, LostGirlFinalStateMachine FSM)
    {
        girl = context;
        fsm = FSM;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    protected void ChangeState(LostGirlState newState)
    {
        Exit();
        newState.Enter();
        girl.currentState = newState;
    }
}
