using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWanderState : ChickenState
{
    public ChickenWanderState(Chicken currentContext, ChickenFinalStateMachine fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Chicken ENTER wander state");
    }

    public override void Execute()
    {
        Vector2 acceleration = chicken.behavior.Wander();
        chicken.behavior.Steer(acceleration);

        if (chicken.isEnemyInSight)
        {
            ChangeState(fsm.PathfinfingState());
        }

    }

    public override void Exit()
    {
        Debug.Log("Chicken EXIT wander state");
    }
}
