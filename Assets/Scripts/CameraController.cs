using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float panSpeed, minCameraHeight, maxCameraHeight, panBorderThickness;
    public Vector2 panLimit;
    public Vector3 cameraPosition;
    public float xposition, yposition, zposition;
    void Start()
    {
        panSpeed = 20f;
        panBorderThickness = 10f;
        panLimit.x = 25;
        panLimit.y = 25;
        minCameraHeight = -5f;
        maxCameraHeight = 10f;
    }

    void Update()
    {
        cameraPosition = transform.position;
        xposition = cameraPosition.x;
        yposition = cameraPosition.y;
        zposition = cameraPosition.z;

        panCamera();
        zoomCamera();

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
            cameraPosition.z += panSpeed * Time.deltaTime;
            cameraPosition.x += panSpeed * Time.deltaTime;
        }

        //Move camera backward
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            cameraPosition.z -= panSpeed * Time.deltaTime;
            cameraPosition.x -= panSpeed * Time.deltaTime;
        }

        //Move camera left
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            cameraPosition.x -= panSpeed * Time.deltaTime;
            cameraPosition.z += panSpeed * Time.deltaTime;
        }

        //Move camera right
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            cameraPosition.x += panSpeed * Time.deltaTime;
            cameraPosition.z -= panSpeed * Time.deltaTime;
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
        //Zoom in
        if (Input.GetKey(KeyCode.UpArrow) && cameraPosition.y > minCameraHeight - 1)
        {
            cameraPosition.z += panSpeed * Time.deltaTime;
            cameraPosition.x += panSpeed * Time.deltaTime;
            cameraPosition.y -= panSpeed * Time.deltaTime;
        }

        //Zoom out
        if (Input.GetKey(KeyCode.DownArrow) && cameraPosition.y < maxCameraHeight - 1)
        {
            cameraPosition.z -= panSpeed * Time.deltaTime;
            cameraPosition.x -= panSpeed * Time.deltaTime;
            cameraPosition.y += panSpeed * Time.deltaTime;
        }
    }
}
