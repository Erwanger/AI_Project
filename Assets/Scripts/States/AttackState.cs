using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    float timer = 0.5f;
    float timerAttack = 0.5f;

    public AttackState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "Attack";
        timer -= Time.deltaTime;

        if (drone.target != null)
        {
            if(drone.aiType == DroneAI.Coward)
            {
                if(drone.GetHp() <= 2)
                {
                    return typeof(FleeState);
                }
            }

            if (drone.aiType == DroneAI.Berserk)
            {
                if (drone.GetHp() <= 5)
                {
                    return typeof(EnrageState);
                }
            }

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
