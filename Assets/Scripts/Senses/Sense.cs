using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    protected Drone owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = gameObject.GetComponent<Drone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
