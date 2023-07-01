using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeState
{
    protected Slime slime;
    protected SlimeFinalState fsm;

    public SlimeState(Slime context, SlimeFinalState FSM)
    {
        slime = context;
        fsm = FSM;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public abstract IEnumerator OnMessage(Message msg);

    protected void ChangeState(SlimeState newState)
    {
        Exit();
        newState.Enter();
        slime.currentState = newState;
    }
}
