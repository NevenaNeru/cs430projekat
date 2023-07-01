using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSeekState : SlimeState
{
    public SlimeSeekState(Slime currentContext, SlimeFinalState fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Slime ENTER seek state");
        slime.behavior.maxAcceleration = 10;
        slime.behavior.maxSpeed = 6;
    }

    public override void Execute()
    {
        Vector2 acceleration = slime.behavior.Seek(slime.wyrm.transform.position);
        slime.behavior.Steer(acceleration);

        if (!slime.isWaterInSight && slime.isPlayerInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }
    }

    public override void Exit()
    {
        slime.behavior.maxAcceleration = 4;
        slime.behavior.maxSpeed = 2;
        Debug.Log("Slime EXIT seek state");
    }

    public override IEnumerator OnMessage(Message msg)
    {
        switch (msg.message)
        {
            case messageType.stopFollow:
                {
                    Debug.Log("Serena gone, don't come!");
                    ChangeState(fsm.WanderState());

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
