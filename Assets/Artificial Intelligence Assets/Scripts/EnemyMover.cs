using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }


    public IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
           Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);

            yield return new WaitForSeconds(1f);

         
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
