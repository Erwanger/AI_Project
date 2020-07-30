using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject gm;
    protected Transform tr;
    protected int nbTick = 0;
    protected Drone drone;
    protected Vector3 destination;

    public BaseState(GameObject _gm, Drone _dr)
    {
        gm = _gm;
        tr = _gm.transform;
        drone = _dr;
    }

    public abstract Type Tick();

    protected Transform CheckForSightTrigger()
    {
        if(gm.GetComponent<Drone>().GetInSightCount() != 0)
        {
            return gm.GetComponent<Drone>().GetFirstInSight();
        }

        return null;
    }

    protected void FindRandomDestination()
    {
        Vector3 testPosition = new Vector3(UnityEngine.Random.Range(-20.5f, 20.5f), 0.0f, UnityEngine.Random.Range(-20.5f, 20.5f));
        destination = new Vector3(testPosition.x, drone.transform.position.y, testPosition.z);

        drone.agent.SetDestination(destination);
    }

}
