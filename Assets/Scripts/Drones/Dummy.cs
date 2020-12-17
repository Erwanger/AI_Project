using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Dummy : Drone //This drone will not move without an order. It's a way to test sight and hearing on a staying drone, or just testing a new state.
{
    // Update is called once per frame
    void Update()
    {
        //In case of problem using the sight, use this part to know if the drone is seeing the right way
        /*foreach(Transform t in objectsInSight)
        {
            Debug.DrawRay(transform.position, t.position, Color.red);
            Debug.Log(gameObject.name + " voit : " + t.name + " à la pos " + t.position);
        }*/
    }

    protected override void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(DummyState), new DummyState(this) },
            { typeof(InvestigateState), new InvestigateState(this) }

        };

        GetComponent<StateMachine>().SetStates(states);
    }

    protected override void InitDroneType()
    {
        aiType = DroneAI.Dummy;
    }

    //If something enter our sight trigger, we add it to the list of "objects in sight"
    protected override void TriggEnter(Collider other)
    {
        if(other.tag != "Sight")
        {
            Drone dr = other.transform.parent.parent.GetComponent<Drone>();
            if (dr != null && dr.team != team)
            {
                //Debug.DrawRay(transform.position, other.transform.position, Color.red);
                objectsInSight.Add(dr.transform);
            }
        }
    }

    //If something get out of our sight trigger, we remove it of the list of "objects in sight"
    protected override void TriggExit(Collider other)
    {
        if (other.tag != "Sight")
        {
            Drone dr = other.transform.parent.parent.GetComponent<Drone>();
            if (dr != null && dr.team != team)
            {
                //Debug.DrawRay(transform.position, other.transform.position, Color.red);
                objectsInSight.Remove(dr.transform);
            }
        }
    }
}
