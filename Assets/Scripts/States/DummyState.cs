using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyState : BaseState
{
    public DummyState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "Dummy";

        Transform chaseTarget = CheckForSightTrigger();
        chaseTarget = null;

        return null;
    }
}
