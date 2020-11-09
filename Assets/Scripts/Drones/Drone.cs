using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public abstract class  Drone : MonoBehaviour
{
    public Team team;
    public DroneAI aiType;

    GameObject fieldOfView;
    protected List<Transform> objectsInSight;

    [SerializeField]protected int hp = 10;

    Transform safeZoneParent;
    [HideInInspector]
    public Transform[] safeZones;

    public int _ID;
    public int tickInIdle;
    public int tickInWander;
    public TextMesh textState;
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public float walkSpeed;
    [HideInInspector]
    public float fleeSpeed;

    public float _chaseRadius; //Si la cible dépasse cette distance, on arrête de chase
    public float _attackRange;
    public float _attackRangeDist;

    [HideInInspector]
    public Transform target;

    public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        fieldOfView = transform.GetChild(0).Find("FieldOfView").gameObject;
        objectsInSight = new List<Transform>();
        InitializeStateMachine();

        walkSpeed = agent.speed;
        fleeSpeed = walkSpeed + 2.0f;
    }

    private void Start()
    {
        safeZoneParent = GameObject.Find("SafeZones").transform;

        safeZones = new Transform[safeZoneParent.childCount];

        for(int i=0; i<safeZoneParent.childCount; i++)
        {
            safeZones[i] = safeZoneParent.GetChild(i);
        }
    }

    private void Update()
    {
        if(hp <= 0)
        {
            Debug.Log("Drone n°" + _ID + " est mort.");
            Destroy(gameObject);
        }
    }

    protected abstract void InitializeStateMachine();

    protected abstract void InitDroneType();

    protected virtual void TriggEnter(Collider other)
    {
        if (other.tag != "Sight")
        {
            Drone dr = other.transform.parent.parent.GetComponent<Drone>();
            if (dr != null && dr.team != team)
            {
                objectsInSight.Add(dr.transform);
            }
        }
    }

    protected virtual void TriggExit(Collider other)
    {
        if (other.tag != "Sight")
        {
            Drone dr = other.transform.parent.parent.GetComponent<Drone>();
            if (dr != null && dr.team != team)
            {
                objectsInSight.Remove(dr.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent)
        {
            if (other.transform.parent.tag == "DroneContent")
                TriggEnter(other);
        }

        if(other.tag == "Sound")
        {
            NavMeshPath path = new NavMeshPath();
            if (gameObject.GetComponent<NavMeshAgent>().CalculatePath(other.transform.position, path))
            {
                gameObject.GetComponent<NavMeshAgent>().SetPath(path);
                StateMachine.ChangeState(typeof(InvestigateState));
            }     
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.parent.tag == "DroneContent")
                TriggExit(other);
        }
    }

    public int GetInSightCount()
    {
        return objectsInSight.Count;
    }

    public Transform GetFirstInSight()
    {
        return objectsInSight[0];
    }

    public void LoseHp()
    {
        hp--;
    }

    public int GetHp()
    {
        return hp;
    }
}

public enum Team
{
    Red,
    Blue
}

public enum DroneAI
{
    Basic,
    Attacker,
    Coward,
    Berserk,
    Dummy,
    NPC,
    Shooter,
    Assault,
    Sniper,
    Farmer
}