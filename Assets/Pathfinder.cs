using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            grid.Add(waypoint.GetGridPosition(), waypoint);
        }
        print(grid.Count);
    }

  
}
