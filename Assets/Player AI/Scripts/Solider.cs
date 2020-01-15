using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

enum State
{
    Dead = 0,
    Alive = 1,
    Combat = 2,
    Patrol = 3,
    Searching = 4,
    DefaultMax = 6 //Total states
}

public class Solider : MonoBehaviour
{

    private State e_state;
    private NavMeshAgent navMeshAgent;

    bool isSelected = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        e_state = State.Alive;
        Brain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Brain()
    {
        if(e_state != State.Dead)
        {
            StartCoroutine(Patrol());
        }
        else
        {
            Destroy(transform);
        }
    }

    IEnumerator Patrol()
    {
        Vector3 newPosition = GenerateNewCoordinates();
        if(!isSelected)
        {
            navMeshAgent.SetDestination(newPosition);
        }
    }

    Vector3 GenerateNewCoordinates()
    {
        Vector3 newPosition = new Vector3(Random.value * 10, 0, Random.value * 10);


        return newPosition;
    }
}
