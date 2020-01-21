using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum GameState
{
   BuildMode = 1,
    FreeMode = 2,
    SqaudMode = 3,
    MoveMode = 4
}

public class GameHandler : MonoBehaviour
{
    private int Mask;
    private int GroundMask;

    [SerializeField] private Quaternion objectRotation;

    private List<NavMeshAgent> navMesh_SelectedTroops;

    private Transform selectedObject;
    private Material highlightedMat;
    [SerializeField] private Transform buildingTransform;



    private Object originalObj;

    GameState gameState;
    
    private void Awake()
    {
        if (navMesh_SelectedTroops == null) navMesh_SelectedTroops = new List<NavMeshAgent>();

        Mask = 1 << 9;
        GroundMask = 1 << 11;
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchGameStates();
        print(navMesh_SelectedTroops.Count);
    }



// private:



    private void SwitchGameStates()
    {
        if(Input.GetKey(KeyCode.F))
        {
            gameState = GameState.FreeMode;
            navMesh_SelectedTroops.Clear();
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameState = GameState.SqaudMode;
            SelectSqaud();
            MoveSelectedSqaud();
        }

        if(Input.GetKey(KeyCode.B))
        {
            navMesh_SelectedTroops.Clear();
            gameState = GameState.BuildMode;
            SpawnBuilding();
        }

    }

    private void SelectSqaud()
    {
   
        if (gameState == GameState.SqaudMode)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mask))
                {
                    NavMeshAgent temp_navMeshAgent = hitInfo.transform.GetComponent<NavMeshAgent>();

                    navMesh_SelectedTroops.Add(temp_navMeshAgent);



                    print("There are now " + navMesh_SelectedTroops.Count + " in the list.");
                }

            }
        }

    }

  
   private void MoveSelectedSqaud()
    { 

        if(navMesh_SelectedTroops.Count != 0)
        {
            if(Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo, Mask))
                {
                    foreach(NavMeshAgent agent in navMesh_SelectedTroops)
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
        else
        {
            Debug.Log("No troops to move..");
        }

    }


    private void SpawnBuilding()
    {
        Ray preBuildRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit preBuildHitInfo;

        if (gameState == GameState.BuildMode)
        {
            if (Physics.Raycast(preBuildRay, out preBuildHitInfo, GroundMask))
            {

                buildingTransform.transform.position = preBuildHitInfo.point;
            }

                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo, GroundMask))
                    {

                        Instantiate(buildingTransform, new Vector3(hitInfo.point.x, hitInfo.point.y + buildingTransform.localScale.y, hitInfo.point.z), objectRotation);

                      
                    }
                }


            }
        
    }

    // public:

    public NavMeshAgent ReturnTopAgentPosition()
    {
        return null;
    }

    public GameState ReturnGameState()
    {
        return gameState;
    }


    public float DstBetweenPoints3D(Vector3 transform_one, Vector3 transform_two)
    {

        float distance = Mathf.Sqrt(
            (transform_one.x - transform_two.x) * (transform_one.x - transform_two.x) +
            (transform_one.y - transform_two.y) * (transform_one.y - transform_two.y) +
            (transform_one.z - transform_two.z) * (transform_one.z - transform_two.z)
            );

        return distance;
    }

}
