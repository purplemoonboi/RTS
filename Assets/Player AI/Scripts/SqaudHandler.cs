using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum GameState
{
    Instantiation = 1,
    FreeMode = 2,
    SqaudMode = 3,
    MoveMode = 4
}

public class SqaudHandler : MonoBehaviour
{
    [SerializeField] private int Mask;
    [SerializeField] private Quaternion objectRotation;

    public List<NavMeshAgent> list_navMeshAgent;

    private Transform selectedObject;
    private Object originalObj;

    GameState gameState;

    private void Awake()
    {
        if (list_navMeshAgent == null) list_navMeshAgent = new List<NavMeshAgent>();
        Mask = 1 << 9;
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateSqaud();
        MoveSelectedSqaud();
        SelectSqaud();
        SelectAllActiveSqauds();

        print(list_navMeshAgent.Count);
    }

    void InstantiateSqaud()
    {
        if (Input.GetKey(KeyCode.I))
        {
            gameState = GameState.Instantiation;
            print(gameState);
        }

        if (gameState == GameState.Instantiation)
        {

            list_navMeshAgent.Clear();

            if (Input.GetMouseButtonDown(0))
            {
                originalObj = FindObjectOfType<Solider>();
                objectRotation = Quaternion.Euler(0, 0, 0);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    Instantiate(originalObj, hitInfo.point, objectRotation);
                }
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            gameState = GameState.FreeMode;
            print(gameState);
        }
    }

    void SelectSqaud()
    { 
        //click on squads to add them to an active group to be moved et cetra.
        if (Input.GetKey(KeyCode.S))
        {
            gameState = GameState.SqaudMode;
            print(gameState);
        }

        if(gameState == GameState.SqaudMode)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo, Mask))
                {
                    NavMeshAgent temp_navMeshAgent = hitInfo.transform.GetComponent<NavMeshAgent>();
                    list_navMeshAgent.Add(temp_navMeshAgent);

                   

                    print("There are now " + list_navMeshAgent.Count + " in the list.");
                }
             
            }   
        }
        else if(Input.GetKey(KeyCode.F))
        {
            gameState = GameState.FreeMode;
            print(gameState);
            list_navMeshAgent.Clear();

        
        }
    }

    void SelectAllActiveSqauds()
    {
        if (Input.GetKey(KeyCode.S))
        {
            gameState = GameState.SqaudMode;
            print(gameState);
        }

        if(Input.GetKey(KeyCode.P))
        {
             //select all 
        }
    }

    void MoveSelectedSqaud()
    {

      

        if (Input.GetKey(KeyCode.M))
        {
            gameState = GameState.MoveMode;
            print(gameState);
        }

        if(list_navMeshAgent.Count != 0)
        {
            if(Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo))
                {
                    foreach(NavMeshAgent agent in list_navMeshAgent)
                    {
                        agent.SetDestination(hitInfo.point);

                        if(DstBetweenPoints3D(agent.transform.position, hitInfo.point) < 20)
                        {
                            agent.speed = 0;
                        }
                        
                    }
                }
            }
        }

    }

    float DstBetweenPoints3D(Vector3 transform_one, Vector3 transform_two)
    {

        float distance = Mathf.Sqrt(
            (transform_one.x - transform_two.x) * (transform_one.x - transform_two.x) + 
            (transform_one.y - transform_two.y) * (transform_one.y - transform_two.y) + 
            (transform_one.z - transform_two.z) * (transform_one.z - transform_two.z)
            );

        return distance;
    }

    public GameState ReturnGameState()
    {
        return gameState;
    }

    public NavMeshAgent ReturnTopAgentPosition()
    {
        return null;
    }

}
