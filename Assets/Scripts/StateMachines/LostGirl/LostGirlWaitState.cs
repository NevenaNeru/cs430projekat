using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostGirlWaitState : LostGirlState
{
    public LostGirlWaitState(LostGirl currentContext, LostGirlFinalStateMachine fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Lost girl ENTER wait state");
    }

    public override void Execute()
    {
        girl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (girl.isPlayerInSight)
        {
            ChangeState(fsm.PursueState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Lost girl EXIT wait state");
    }
}
