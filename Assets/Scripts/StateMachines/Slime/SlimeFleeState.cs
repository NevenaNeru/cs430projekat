using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFleeState : SlimeState
{
    public SlimeFleeState(Slime currentContext, SlimeFinalState fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Slime ENTER flee state");
    }

    public override void Execute()
    {
        Vector2 acceleration = slime.behavior.Flee(slime.water.transform.position);
        slime.behavior.Steer(acceleration);

        if (!slime.isWaterInSight && !slime.isPlayerInSight)
        {
            ChangeState(fsm.WanderState());
        }

        if (!slime.isWaterInSight && slime.isPlayerInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Slime EXIT flee state");
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
