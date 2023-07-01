using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmPathFollowState : WyrmStates
{
    public Path path;
    public WyrmPathFollowState(Wyrm currentContext, WyrmFinalState fsm) : base(currentContext, fsm)
    {
        wyrm = currentContext;
    }

    public override void Enter()
    {
        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime1.myReciverId, messageType.stopSlime);
        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime2.myReciverId, messageType.stopSlime);
        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime3.myReciverId, messageType.stopSlime);

        Debug.Log("Wyrm ENTER path state");
        path = wyrm.path;
    }

    public override void Execute()
    {
        Vector2 acceleration = wyrm.behaviour.PathFollow(path);
        wyrm.behaviour.Steer(acceleration);

        if (wyrm.isAttacking)
        {
            ChangeState(fsm.AttackState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Wyrm EXIT path state");
    }

    public override IEnumerator OnMessage(Message msg)
    {
        yield return new WaitForSeconds(0.1f);
    }
}
