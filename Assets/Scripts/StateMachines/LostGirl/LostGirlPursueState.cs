using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LostGirlPursueState : LostGirlState
{
    public Player player;
    Rigidbody2D rb;
    public LostGirlPursueState(LostGirl currentContext, LostGirlFinalStateMachine fsm) : base(currentContext, fsm)
    {
        girl = currentContext;
    }

    public override void Enter()
    {
        Debug.Log("Lost girl ENTER pursuit state");
        player = girl.player;
        rb = player.GetComponent<Rigidbody2D>();
    }

    public override void Execute()
    {
        Vector2 acceleration = girl.behaviour.Pursuit(rb);
        girl.behaviour.Steer(acceleration);

        if (girl.isHouseInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Lost girl EXIT pursuit state");
    }
}
