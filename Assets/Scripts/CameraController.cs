using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameHandler))]
public class CameraController : MonoBehaviour
{
   
    [SerializeField] private float panSpeed, minCameraHeight, maxCameraHeight, panBorderThickness, rotateSpeed;
    [SerializeField] private Vector2 panLimit;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 targetOffset;


    [SerializeField] private Transform cameraTarget;
    [SerializeField] private GameObject camera;
   
    private int layerMask = 1 << 9;
    private bool isFollowing;

    void Start()
    {
        panSpeed = 20f;
        panBorderThickness = 10f;
        panLimit.x = 25;
        panLimit.y = 25;
        minCameraHeight = -5f;
        maxCameraHeight = 10f;
        rotateSpeed = 10f;
        targetOffset.x = 15f;
        isFollowing = false;

    }

    void Update()
    {
        cameraPosition = transform.position;
    

        ZoomCamera();
        PanCamera();
        RotateCamera();

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -panLimit.x, panLimit.x);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -panLimit.y, panLimit.y);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minCameraHeight, maxCameraHeight);

        transform.position = cameraPosition;
    }

    void PanCamera()
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

    void ZoomCamera()
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

    void RotateCamera()
    {
        

        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 20f))
            {

                transform.RotateAround(hitInfo.point, Vector3.up, 30 * Time.deltaTime);

            }
        }

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
           

            if (Physics.Raycast(ray, out hitInfo, 20f))
            {

                transform.RotateAround(hitInfo.point, Vector3.up, -30 * Time.deltaTime);


            }
        }

       
    }

    void FollowTarget()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, layerMask))
            {
                cameraTarget = hitInfo.transform.GetComponent<Transform>();
                isFollowing = true;
                transform.position = cameraTarget.position + targetOffset;
            }
            else if(!Physics.Raycast(ray, out hitInfo, layerMask))
            {
                isFollowing = false;
            }
        }
    }
}