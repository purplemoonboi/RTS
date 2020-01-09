﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float panSpeed, minCameraHeight, maxCameraHeight, panBorderThickness, rotateSpeed;
    public Vector2 panLimit;
    public Vector3 cameraPosition;
    public float xposition, yposition, zposition;
    public GameObject cameraTarget;
    public GameObject camera;

    void Start()
    {
        panSpeed = 20f;
        panBorderThickness = 10f;
        panLimit.x = 25;
        panLimit.y = 25;
        minCameraHeight = -5f;
        maxCameraHeight = 10f;
        rotateSpeed = 10f;
    }

    void Update()
    {
        cameraPosition = transform.position;
        xposition = cameraPosition.x;
        yposition = cameraPosition.y;
        zposition = cameraPosition.z;

        zoomCamera();
        panCamera();
        rotateCamera();

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -panLimit.x, panLimit.x);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -panLimit.y, panLimit.y);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minCameraHeight, maxCameraHeight);

        transform.position = cameraPosition;
    }

    void panCamera()
    {
        //Move camera forward
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            cameraPosition.z += camera.transform.forward.z * panSpeed * Time.deltaTime;
            cameraPosition.x += camera.transform.forward.x * panSpeed * Time.deltaTime;
        }

        //Move camera backward
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            cameraPosition.z -= camera.transform.forward.z * panSpeed * Time.deltaTime;
            cameraPosition.x -= camera.transform.forward.x * panSpeed * Time.deltaTime;
        }

        //Move camera left
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            cameraPosition.x -= camera.transform.right.x * panSpeed * Time.deltaTime;
            cameraPosition.z += -(camera.transform.right.z) * panSpeed * Time.deltaTime;
        }

        //Move camera right
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            cameraPosition.x += camera.transform.right.x * panSpeed * Time.deltaTime;
            cameraPosition.z -= -(camera.transform.right.z) * panSpeed * Time.deltaTime;
        }

        //Move camera down
        if (Input.GetKey(KeyCode.Q))
        {
            cameraPosition.y -= panSpeed * Time.deltaTime;
        }

        //Move camera up
        if (Input.GetKey(KeyCode.E))
        {
            cameraPosition.y += panSpeed * Time.deltaTime;
        }
    }

    void zoomCamera()
    {
        Vector3 cameraZoom = camera.transform.position;

        //Zoom in
        if (Input.GetKey(KeyCode.UpArrow) && cameraZoom.y > minCameraHeight + 1)
        {
            cameraZoom.z += camera.transform.forward.z * panSpeed * Time.deltaTime;
            cameraZoom.x += camera.transform.forward.x * panSpeed * Time.deltaTime;
            cameraZoom.y -= panSpeed * Time.deltaTime;
        }

        //Zoom out
        if (Input.GetKey(KeyCode.DownArrow) && cameraZoom.y < maxCameraHeight - 1)
        {
            cameraZoom.z -= camera.transform.forward.z * panSpeed * Time.deltaTime;
            cameraZoom.x -= camera.transform.forward.x * panSpeed * Time.deltaTime;
            cameraZoom.y += panSpeed * Time.deltaTime;
        }

        camera.transform.position = cameraZoom;
    }

    void rotateCamera()
    {
        //Rotate camera left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
           
        }

        //Rotate camera right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, -(rotateSpeed * Time.deltaTime), 0);
        }
    }
}