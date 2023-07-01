using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWanderState : SlimeState
{
    public SlimeWanderState(Slime currentContext, SlimeFinalState fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        slime.behavior.maxAcceleration = 4;
        slime.behavior.maxSpeed = 2;
        Debug.Log("Slime ENTER wander state");
    }

    public override void Execute()
    {
        Vector2 acceleration = slime.behavior.Wander();
        slime.behavior.Steer(acceleration);

        if (slime.isPlayerInSight && !slime.isWaterInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }

        if (slime.isWaterInSight)
        {
            ChangeState(fsm.FleeState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Slime EXIT wander state");
    }

    public override IEnumerator OnMessage(Message msg)
    {
        switch (msg.message)
        {
            case messageType.callSlime:
                {
                    Debug.Log("Serena spotted, come here!");
                    ChangeState(fsm.SeekState());

                    break;
                }

            default:
                {
                    break;
                }
        }
        yield return new WaitForSeconds(0.1f);
    }
}
