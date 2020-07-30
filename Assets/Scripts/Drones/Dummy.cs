using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Dummy : Drone
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Drone n°" + _ID + " : " +  objectsInSight.Count);
        foreach(Transform t in objectsInSight)
        {
            //Debug.DrawRay(transform.position, t.position, Color.red);
            Debug.Log(gameObject.name + " voit : " + t.name + " à la pos " + t.position);
        }
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
