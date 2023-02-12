using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object that the camera will follow
    public float camSensitivity = 0.5f; // The speed at which the camera will move
    float edgeThreshold = 1f; // The distance from the screen edge where the camera will start 
    float camLimitVertical = 24;
    float camLimitHorizontal = 24;
    
    Transform cam;
    
    void Start()
    {
        cam = GetComponent<Transform>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            cam.transform.position = new Vector3(target.position.x, cam.transform.position.y, target.position.z - 22f);
        }
        MoveUp();
        MoveDown();
        MoveLeft();
        MoveRight();
    }

    void MoveUp()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = Screen.height;
        if(mousePos.y - edgeThreshold >= height && cam.transform.position.z <= 128 - camLimitVertical)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + camSensitivity);
        }
    }

    void MoveDown()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = Screen.height;
        if(mousePos.y <= edgeThreshold && cam.transform.position.z >= -camLimitVertical)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z - camSensitivity);
        }
    }

    void MoveLeft()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = Screen.height;
        if(mousePos.x <= edgeThreshold && cam.transform.position.x >= camLimitHorizontal)
        {
            cam.transform.position = new Vector3(cam.transform.position.x -camSensitivity, cam.transform.position.y, cam.transform.position.z);
        }
    }

    void MoveRight()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = Screen.height;
        if(mousePos.x - edgeThreshold >= Screen.width && cam.transform.position.x <= 128 - camLimitHorizontal)
        {
            cam.transform.position = new Vector3(cam.transform.position.x +camSensitivity, cam.transform.position.y, cam.transform.position.z);
        }
    }
}
