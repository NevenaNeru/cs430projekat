using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChickenState
{
    protected Chicken chicken;
    protected ChickenFinalStateMachine fsm;

    public ChickenState(Chicken context, ChickenFinalStateMachine FSM)
    {
        chicken = context;
        fsm = FSM;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    protected void ChangeState(ChickenState newState)
    {
        Exit();
        newState.Enter();
        chicken.currentState = newState;
    }
}
