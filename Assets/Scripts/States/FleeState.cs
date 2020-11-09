using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    Transform chaseTarget;

    public FleeState(Drone _dr) : base(_dr.gameObject, _dr)
    {
        chaseTarget = null;
    }

    public override Type Tick()
    {
        drone.textState.text = "Flee";

        drone.agent.speed = drone.fleeSpeed;
        chaseTarget = CheckForSightTrigger();

        if (drone.aiType == DroneAI.Farmer)
        {
            if (chaseTarget != null)
            {
                //FindRandomDestination();
                GoToSafeZone();
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

    protected override void FindRandomDestination()
    {
        if(chaseTarget)
        {
            float angle = Vector3.Angle(drone.transform.forward, chaseTarget.position - drone.transform.position);
           
            Vector3 testPosition = new Vector3(UnityEngine.Random.Range(-20.5f, 20.5f), 0.0f, UnityEngine.Random.Range(-20.5f, 20.5f));

            float angle2 = Vector3.Angle(testPosition - drone.transform.position, chaseTarget.position - drone.transform.position);

            while(Mathf.Abs(angle2-angle) < 40.0f)
            {
                testPosition = new Vector3(UnityEngine.Random.Range(-20.5f, 20.5f), 0.0f, UnityEngine.Random.Range(-20.5f, 20.5f));
                angle = Vector3.Angle(drone.transform.forward, chaseTarget.position - drone.transform.position);
                angle2 = Vector3.Angle(testPosition - drone.transform.position, chaseTarget.position - drone.transform.position);
            }

            destination = new Vector3(testPosition.x, drone.transform.position.y, testPosition.z);

            drone.agent.SetDestination(destination);
        }
    }

    private void GoToSafeZone()
    {
        drone.agent.SetDestination(SafeZone());
    }


    private Vector3 SafeZone()
    {
        float distance = 999.0f;
        float tmp = 0.0f;
        int safeIndex = 999;

        for(int i=0; i<drone.safeZones.Length; i++)
        {
            tmp = Vector3.Magnitude(drone.safeZones[i].position - drone.transform.position);

            if(tmp<distance)
            {
                distance = tmp;
                safeIndex = i;
            }
        }

        return drone.safeZones[safeIndex].position;
    }
}
