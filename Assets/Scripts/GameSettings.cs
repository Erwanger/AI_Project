using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A singleton that is plan to regroup every parameters that needs to be global
public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public float droneSpeed;
    public float aggroRadius;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
