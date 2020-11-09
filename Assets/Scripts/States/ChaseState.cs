using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{

    public ChaseState(Drone _dr) : base(_dr.gameObject, _dr)
    {

    }

    public override Type Tick()
    {
        /*Le drone doit poursuivre la cible jusqu'a être en position d'attaque*/
        drone.textState.text = "Chase";

        if(drone.target != null)
        {
            if (IsAtRangeAttack() && (drone.aiType != DroneAI.Shooter && drone.aiType != DroneAI.Sniper))
            {
                drone.agent.ResetPath();
                if (drone.aiType == DroneAI.Berserk)
                {
                    if (drone.GetHp() <= 5)
                    {
                        return typeof(EnrageState);
                    }
                }

                return typeof(AttackState);
            }
            else if (IsAtRangeAttackDist() && (drone.aiType == DroneAI.Assault || drone.aiType == DroneAI.Shooter || drone.aiType == DroneAI.Sniper))
            {
                drone.agent.ResetPath();
                return typeof(AttackDistState);
            }
            else if (IsAtRangeChase())
            {
                drone.agent.SetDestination(drone.target.position);
            }
            else
            {
                drone.target = null;
                return typeof(WanderState);
            }
        }
        else
        {
            return typeof(WanderState);
        }

        

        return null;
    }

    private bool IsAtRangeAttack()
    {
        if (drone._attackRange > (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }

    private bool IsAtRangeAttackDist()
    {
        if (drone._attackRangeDist > (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }

    private bool IsAtRangeChase()
    {
        if (drone._chaseRadius >= (drone.transform.position - drone.target.position).magnitude)
        {
            return true;
        }

        return false;
    }
}
