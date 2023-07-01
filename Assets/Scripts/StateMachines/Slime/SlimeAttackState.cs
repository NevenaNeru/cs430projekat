using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : SlimeState
{
    public SlimeAttackState(Slime currentContext, SlimeFinalState fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Slime ENTER attack state");
    }

    public override void Execute()
    {
        slime.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        slime.player.TakeDamage(1);

        //Debug.Log(slime.player.currentHealth);

        if (!slime.isAttacking && slime.isPlayerInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Slime EXIT attack state");
    }

    public override IEnumerator OnMessage(Message msg)
    {
        yield return new WaitForSeconds(0.1f);
    }
}
