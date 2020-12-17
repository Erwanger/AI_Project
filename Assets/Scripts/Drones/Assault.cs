using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Assault : Drone  //This drone will act like a range one, but will do melee attacks when to close
{

    protected override void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(AttackState), new AttackState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(InvestigateState), new InvestigateState(this) },
            { typeof(AttackDistState), new AttackDistState(this) },
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    protected override void InitDroneType()
    {
        aiType = DroneAI.Assault;
    }
}
