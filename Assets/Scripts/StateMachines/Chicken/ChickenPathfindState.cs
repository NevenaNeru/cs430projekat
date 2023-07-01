using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPathfindState : ChickenState
{
    public ChickenPathfindState(Chicken currentContext, ChickenFinalStateMachine fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        if (!chicken.grid.walls.Contains(chicken.transform.position) &&
            chicken.grid.inGrid(chicken.transform.position))
        {
            float x = Mathf.Round(chicken.transform.position.x);
            float y = Mathf.Round(chicken.transform.position.y);
            chicken.grid.start = new Vector2(x, y);
        }
        chicken.behavior = chicken.GetComponent<SteeringBehaviour>();
        chicken.path = chicken.GetComponent<Path>();

        Debug.Log("Chicken ENTER pathfinding state");
    }

    public override void Execute()
    {
        float x = Mathf.Round(chicken.transform.position.x);
        float y = Mathf.Round(chicken.transform.position.y);
        chicken.grid.start = new Vector2(x, y);

        float houseX = Mathf.Round(chicken.house.transform.position.x);
        float houseY = Mathf.Round(chicken.house.transform.position.y);
        chicken.grid.end = new Vector2(houseX, houseY);

        List<Node> AstarPath = Pathfinding.AStar(chicken.grid, chicken.grid.start, chicken.grid.end);

        if (AstarPath.Count > 1)
        {
            chicken.path.pathPoint = AstarPath;
            Vector2 acceleration = chicken.behavior.PathFollow(chicken.path);
            chicken.behavior.Steer(acceleration);
        }

        if (!chicken.isEnemyInSight)
        {
            ChangeState(fsm.WanderState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Chicken EXIT pathfinding state");
    }
}
