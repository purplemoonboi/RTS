using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class EditorSnap : MonoBehaviour
{

    [Header("Grid Size")] [SerializeField] [Range(10,100)] float gridSize = 10f;

    TextMesh textMesh;

    private void Start()
    {
       
    }

    void Update()
    {

        textMesh = GetComponentInChildren<TextMesh>();
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x /gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);

        string labelText = snapPos.x / gridSize + ", " + snapPos.z / gridSize;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
