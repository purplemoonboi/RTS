using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour
{

    [SerializeField] private float buildingIntergrity;
    private Collider thisBuildingsCollider;


    // Start is called before the first frame update
    void Start()
    {
        thisBuildingsCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }



    void Destroy()
    {
        if (buildingIntergrity < 0)
        {
            Destroy(GameObject.Find("Barracks"));
        }
    }

    
}
