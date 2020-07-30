using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDistState : BaseState
{
    float timer = 1.0f;
    float timerAttack = 1.0f;

    public AttackDistState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        drone.textState.text = "AttackDist";
        timer -= Time.deltaTime;
        if (drone.target != null)
        {

            if (IsAtRange() && drone.aiType != DroneAI.Shooter)
            {
                if (drone.aiType == DroneAI.Assault)
                {
                    return typeof(AttackState);
                }
                else if (drone.aiType == DroneAI.Sniper)
                {
                    return typeof(FleeState);
                }
            }
            else if (IsAtRangeDist())
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
        if (drone._attackRange > (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }

    private void Attack()
    {
        Debug.Log("Drone n°" + drone._ID + " hit!");
        drone.target.GetComponent<Drone>().LoseHp();
    }

    private bool IsAtRangeDist()
    {
        if (drone._attackRangeDist > (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }
}
