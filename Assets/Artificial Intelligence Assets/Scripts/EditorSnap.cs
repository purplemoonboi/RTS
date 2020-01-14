using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{

   

    TextMesh textMesh;
    Waypoint waypoint;
   

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(waypoint.GetGridPosition().x, 0f, waypoint.GetGridPosition().y);
    }

    void UpdateLabel()
    {

        textMesh = GetComponentInChildren<TextMesh>();

    
        int gridSize = waypoint.GetGridSize();
        string labelText = waypoint.GetGridPosition().x / gridSize + ", " + waypoint.GetGridPosition().y / gridSize;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
