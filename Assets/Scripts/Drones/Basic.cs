﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Basic : Drone
{
    protected override void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(InvestigateState), new InvestigateState(this)}
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    protected override void InitDroneType()
    {
        aiType = DroneAI.Basic;
    }
}
