using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageState : BaseState
{
    float timer = 0.25f;
    float timerAttack = 0.25f;

    public EnrageState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "ENRAGE";
        timer -= Time.deltaTime;

        if (drone.target != null)
        {
            if (IsAtRange())
            {
                drone.transform.forward = drone.target.position - drone.transform.position;

                if (timer <= 0.0f)
                {
                    Attack();
                    timer = timerAttack;
                }
            }
            else
            {
                return typeof(ChaseState);
            }
        }
        else
        {
            return typeof(WanderState);
        }

        return null;
    }

    private bool IsAtRange()
    {
        if (drone.AttackRange > (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }

    private void Attack()
    {
        Debug.Log("Drone n°" + drone.GetId() + " hit!");
        drone.target.GetComponent<Drone>().LoseHp();
    }
}
