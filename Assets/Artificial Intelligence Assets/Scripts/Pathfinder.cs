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

        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPosition();

            if (grid.ContainsKey(gridPos)) 
            {
                Debug.LogWarning("Overlapping Block!" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                waypoint.SetColour(Color.yellow);
            }
        }
        print("Loaded" + grid.Count + " Blocks.");
        
    }

  
}
