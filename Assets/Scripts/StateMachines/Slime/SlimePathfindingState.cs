using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePathfindingState : SlimeState
{
    public SlimePathfindingState(Slime currentContext, SlimeFinalState fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        if (!slime.grid.walls.Contains(slime.transform.position) && 
            slime.grid.inGrid(slime.transform.position))
        {
            float x = Mathf.Round(slime.transform.position.x);
            float y = Mathf.Round(slime.transform.position.y);
            slime.grid.start = new Vector2(x, y);
        }
        slime.behavior = slime.GetComponent<SteeringBehaviour>();
        slime.path = slime.GetComponent<Path>();

        slime.behavior.maxAcceleration = 6;
        slime.behavior.maxSpeed = 3;

        Debug.Log("Slime ENTER pathfinding state");
    }

    public override void Execute()
    {
        float x = Mathf.Round(slime.transform.position.x);
        float y = Mathf.Round(slime.transform.position.y);
        slime.grid.start = new Vector2(x, y);

        float playerx = Mathf.Round(slime.player.transform.position.x);
        float playery = Mathf.Round(slime.player.transform.position.y);
        slime.grid.end = new Vector2(playerx, playery);

        List<Node> AstarPath = Pathfinding.AStar(slime.grid, slime.grid.start, slime.grid.end);

        if (AstarPath.Count > 1)
        {
            slime.path.pathPoint = AstarPath;
            Vector2 acceleration = slime.behavior.PathFollow(slime.path);
            slime.behavior.Steer(acceleration);
        }

        if (!slime.isPlayerInSight && !slime.isWaterInSight)
        {
            ChangeState(fsm.WanderState());
        }

        if (slime.isAttacking)
        {
            ChangeState(fsm.AttackState());
        }
        if (slime.isWaterInSight)
        {
            ChangeState(fsm.FleeState());
        }
    }

    public override void Exit()
    {
        slime.behavior.maxAcceleration = 4;
        slime.behavior.maxSpeed = 2;
        Debug.Log("Slime EXIT pathfinding state");
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
