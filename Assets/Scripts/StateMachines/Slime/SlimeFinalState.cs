using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFinalState
{
    Slime context;

    public SlimeFinalState(Slime current)
    {
        context = current;
    }

    public SlimeState PathfinfingState()
    {
        return new SlimePathfindingState(context, this);
    }
    public SlimeState FleeState()
    {
        return new SlimeFleeState(context, this);
    }
    public SlimeState WanderState()
    {
        return new SlimeWanderState(context, this);
    }

    public SlimeState AttackState()
    {
        return new SlimeAttackState(context, this);
    }

    public SlimeState SeekState()
    {
        return new SlimeSeekState(context, this);
    }
}
