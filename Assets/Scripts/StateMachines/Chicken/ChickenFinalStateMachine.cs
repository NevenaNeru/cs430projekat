using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFinalStateMachine : MonoBehaviour
{
    Chicken context;

    public ChickenFinalStateMachine(Chicken current)
    {
        context = current;
    }
    public ChickenState PathfinfingState()
    {
        return new ChickenPathfindState(context, this);
    }

    public ChickenState WanderState()
    {
        return new ChickenWanderState(context, this);
    }
}
