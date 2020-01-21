using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantrySpawner : MonoBehaviour
{

    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform troopTransform;

    private int referenceNumber = 0;

    [SerializeField] private Dictionary<int, Solider> troopRecord = new Dictionary<int, Solider>();

    // Start is called before the first frame update
    void Start()
    {
        Spawner();
    }


    private void Spawner()
    {
       if(GameObject.Find("Barracks") != null)
        {
            print("This building's Location" + transform.position);

            StartCoroutine(SpawnTroops());
        }
       else
        {
            Debug.LogWarning("Cannot Find object Barracks");
        }
    }
    
    IEnumerator SpawnTroops()
    {
        print("Is spawning");

       var Object = Instantiate(troopTransform, spawnPos.transform.position, Quaternion.Euler(0, 0, 0));


       // troopRecord.Add( ,Object)

        yield return new WaitForSeconds(10f);

        if (GameObject.Find("Barracks") != null)
        {
            StartCoroutine(SpawnTroops());
        }
    }

    private int TroopTag(int Soldier_ID)
    {
        Soldier_ID = Random.Range(0, 100);

        return Soldier_ID;
    }

    
}
