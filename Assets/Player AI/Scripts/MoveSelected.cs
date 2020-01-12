using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveSelected : MonoBehaviour
{

    [SerializeField] private Material highlightedMaterial;

    private Transform selectedObject;
    NavMeshAgent selectedNavMeshAgent;
    private bool bSelected;

    // Update is called once per frame
    void Update()
    {
        SelectedGameObject();
        SetTargetPosition();
     
    }

    void SelectedGameObject()
    {
       
        int LayerMask_Infantry = 9;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, LayerMask_Infantry))
            {
                selectedNavMeshAgent = hit.transform.GetComponent<NavMeshAgent>();
            }
            else
            {
                bSelected = false;
            }
        }

    }

    void SetTargetPosition()
    {
        if(bSelected)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetButtonDown("Fire1"))
            {

                if (Physics.Raycast(ray, out hit))
                {
                    selectedNavMeshAgent.SetDestination(hit.point);
                }
              
                

            }
        }
        if(!bSelected)
        {
            selectedNavMeshAgent = null;
        }
     
    }


  
}
