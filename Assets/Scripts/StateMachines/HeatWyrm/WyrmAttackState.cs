using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmAttackState : WyrmStates
{
    public WyrmAttackState(Wyrm context, WyrmFinalState FSM) : base(context, FSM)
    {
    }

    public override void Enter()
    {
        Debug.Log("Wyrm ENTER attack state");

        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime1.myReciverId, messageType.callSlime);
        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime2.myReciverId, messageType.callSlime);
        MessageDispatcher.Instance().Dispatch(0.0f, wyrm.mySenderId, wyrm.slime3.myReciverId, messageType.callSlime);
    }

    public override void Execute()
    {
        wyrm.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        wyrm.player.TakeDamage(2);

        //Debug.Log(wyrm.player.currentHealth);

        if (!wyrm.isAttacking)
        {
            ChangeState(fsm.PathFollowState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Wyrm EXIT attack state");
    }

    public override IEnumerator OnMessage(Message msg)
    {
        yield return new WaitForSeconds(0.1f);
    }
}
