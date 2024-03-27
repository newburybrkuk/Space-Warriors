using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveTo : MonoBehaviour
{
    private GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindWithTag("Player");

        if (goal is not null)
        {
            agent.destination = goal.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
