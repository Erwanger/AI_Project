using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : BaseState
{
    private float timer = 0.0f;
    private readonly float timeToWait = 5.0f;

    public InvestigateState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "Investigate";

        Transform chaseTarget = CheckForSightTrigger();

        if (drone.aiType == DroneAI.Attacker || drone.aiType == DroneAI.Coward || drone.aiType == DroneAI.Berserk || 
            drone.aiType == DroneAI.Shooter || drone.aiType == DroneAI.Assault || drone.aiType == DroneAI.Sniper)
        {
            if (chaseTarget != null)
            {
                drone.target = chaseTarget;
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



        if (drone.agent.remainingDistance <= 0.5f)
        {
            timer += Time.deltaTime;

            if(timer >= timeToWait)
            {
                timer = 0.0f;
                if(drone.aiType == DroneAI.Dummy)
                {
                    return typeof(DummyState);
                }
                return typeof(WanderState);
            }
        }

        return null;
    }
}
