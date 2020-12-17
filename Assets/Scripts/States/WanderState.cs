using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    public WanderState(Drone _dr) : base(_dr.gameObject, _dr)
    {
     
    }

    public override Type Tick()
    {
        drone.textState.text = "Wander";

        Transform chaseTarget = CheckForSightTrigger();

        if(drone.aiType == DroneAI.Attacker || drone.aiType == DroneAI.Coward || drone.aiType == DroneAI.Berserk)
        {
            if (chaseTarget != null)
            {
                drone.target = chaseTarget;
                drone.transform.forward = drone.target.position - drone.transform.position;
                return typeof(ChaseState);
            }
        }

        if (drone.aiType == DroneAI.Farmer)
        {
            if (chaseTarget != null)
            {
                return typeof(FleeState);
            }
        }


        if (!drone.agent.pathPending && drone.agent.remainingDistance < 0.5f)
        {
            //We find a new destination
            FindRandomDestination();

            //or we can go on Idle
            if (nbTick >= drone.tickInWander)
            {
                nbTick = 0;
                return typeof(IdleState);
            }

            nbTick++;
        }

        return null;
    }
}
