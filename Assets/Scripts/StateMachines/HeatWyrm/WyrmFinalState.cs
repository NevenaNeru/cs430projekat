using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyrmFinalState
{
    Wyrm context;

    public WyrmFinalState(Wyrm current)
    {
        context = current;
    }

    public WyrmStates PathFollowState()
    {
        return new WyrmPathFollowState(context, this);
    }


    public WyrmStates AttackState()
    {
        return new WyrmAttackState(context, this);
    }
}
