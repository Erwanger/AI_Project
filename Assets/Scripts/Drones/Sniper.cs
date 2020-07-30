using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Sniper : Drone
{

    protected override void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(AttackDistState), new AttackDistState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(InvestigateState), new InvestigateState(this) },
            { typeof(FleeState), new FleeState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    protected override void InitDroneType()
    {
        aiType = DroneAI.Sniper;
    }
}
