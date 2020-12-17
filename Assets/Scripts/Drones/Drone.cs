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

    [SerializeField]
    int _ID = 0;
    public int tickInIdle;
    public int tickInWander;
    public TextMesh textState;
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public float walkSpeed;
    [HideInInspector]
    public float fleeSpeed;

    [SerializeField]
    float _chaseRadius = 10.0f; //If the target is further than this radius, we stop the chase
    [SerializeField]
    float _attackRange = 2.0f;
    [SerializeField]
    float _attackRangeDist = 10.0f;

    [HideInInspector]
    public Transform target;

    public StateMachine StateMachine => GetComponent<StateMachine>();

    public float ChaseRadius => _chaseRadius;
    public float AttackRange => _attackRange;
    public float AttackRangeDist  => _attackRangeDist;

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
        //The drone will save the SafeZones
        safeZoneParent = GameObject.Find("SafeZones").transform;

        safeZones = new Transform[safeZoneParent.childCount];

        for(int i=0; i<safeZoneParent.childCount; i++)
        {
            safeZones[i] = safeZoneParent.GetChild(i);
        }
    }

    // Update is called once per frame
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

    //If something enter our sight trigger, we add it to the list of "objects in sight"
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

    //If something get out of our sight trigger, we remove it of the list of "objects in sight"
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


    //If a drone enter a trigger (the sight one), we call the method needed. If it's a sound, we're going into "InvestigateState"
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
        StartCoroutine("GetHit");
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetId()
    {
        return _ID;
    }

    IEnumerator GetHit()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        yield return new WaitForSeconds(0.1f);

        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
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