using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveSelected : MonoBehaviour
{
    [SerializeField] Transform targetLocation;
    NavMeshAgent navMeshAgent;



    // Start is called before the first frame update
    void Start()
    {
         navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetLocation.position;
    }

    void SelectGameObject()
    {
        //select a game obj to manipulate.
    }

    void SetTargetPosition()
    {
        //set the loc for the game obj to move to.
    }
}
