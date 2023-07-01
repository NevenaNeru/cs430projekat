using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LostGirlPathfindingState : LostGirlState
{
    public LostGirlPathfindingState(LostGirl currentContext, LostGirlFinalStateMachine fsm) : base(currentContext, fsm)
    {
    }

    public override void Enter()
    {
        if (!girl.grid.walls.Contains(girl.transform.position) &&
            girl.grid.inGrid(girl.transform.position))
        {
            float x = Mathf.Round(girl.transform.position.x);
            float y = Mathf.Round(girl.transform.position.y);
            girl.grid.start = new Vector2(x, y);
        }
        girl.behaviour = girl.GetComponent<SteeringBehaviour>();
        girl.path = girl.GetComponent<Path>();

        Debug.Log("Lost girl ENTER pathfinding state");
    }

    public override void Execute()
    {
        float x = Mathf.Round(girl.transform.position.x);
        float y = Mathf.Round(girl.transform.position.y);
        girl.grid.start = new Vector2(x, y);

        float houseX = Mathf.Round(girl.house.transform.position.x);
        float houseY = Mathf.Round(girl.house.transform.position.y);
        girl.grid.end = new Vector2(houseX, houseY);

        List<Node> AstarPath = Pathfinding.AStar(girl.grid, girl.grid.start, girl.grid.end);

        if (AstarPath.Count > 1)
        {
            girl.path.pathPoint = AstarPath;
            Vector2 acceleration = girl.behaviour.PathFollow(girl.path);
            girl.behaviour.Steer(acceleration);
        }
        
        if (girl.houseCollision)
        {
            //something about exiting this state so i don't get null :)
        }
    }

    public override void Exit()
    {
        Debug.Log("Lost girl EXIT pathfinding state");
    }
}
