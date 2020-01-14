using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinder : MonoBehaviour
{

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        SetStartEndColour();
        ExploreNeighbour();
    }

    private void SetStartEndColour()
    {
        startWaypoint.SetColour(Color.blue);
        endWaypoint.SetColour(Color.red);
    }

    private void ExploreNeighbour()
    {
        foreach(Vector2Int direction in directions)
        {
            Vector2Int explorationCoorinates = startWaypoint.GetGridPosition() + direction;
            print("Exploring " + startWaypoint.GetGridPosition() + direction);
            grid[explorationCoorinates].SetColour(Color.black);
        }
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
                
            }
        }
        print("Loaded" + grid.Count + " Blocks.");
        
    }

  
}
