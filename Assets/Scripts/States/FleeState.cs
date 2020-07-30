using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    public FleeState(Drone _dr) : base(_dr.gameObject, _dr)
    {
        
    }

    public override Type Tick()
    {
        drone.textState.text = "Flee";

        drone.agent.speed = drone.fleeSpeed;
        Transform chaseTarget = CheckForSightTrigger();

        if (drone.aiType == DroneAI.Farmer)
        {
            if (chaseTarget != null)
            {
                FindRandomDestination();
            }
        }

        if (!drone.agent.pathPending && drone.agent.remainingDistance < 0.5f)
        {
            //soit on trouve une nouvelle destination
            FindRandomDestination();

            //soit on bascule en Idle
            if (nbTick >= drone.tickInWander)
            {
                nbTick = 0;
                drone.agent.speed = drone.walkSpeed;
                return typeof(WanderState);
            }

            nbTick++;
        }

        return null;
    }
}
