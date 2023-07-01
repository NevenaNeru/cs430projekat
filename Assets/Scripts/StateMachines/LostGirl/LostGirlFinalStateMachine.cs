using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostGirlFinalStateMachine
{
    LostGirl context;

    public LostGirlFinalStateMachine(LostGirl current)
    {
        context = current;
    }

    public LostGirlState WaitState()
    {
        return new LostGirlWaitState(context, this);
    }

    public LostGirlState PathfinfingState()
    {
        return new LostGirlPathfindingState(context, this);
    }

    public LostGirlState PursueState()
    {
        return new LostGirlPursueState(context, this);
    }
}
