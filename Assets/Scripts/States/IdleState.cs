using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "Idle";

        if(nbTick >= drone.tickInIdle)
        {
            nbTick = 0;
            return typeof(WanderState);
        }

        nbTick++;
        return null;
    }

}
