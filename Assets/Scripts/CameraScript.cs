using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float degreeLimit = 45.0f;
    private float speed = 0.2f;
    private int edgeOffset = 30;
    private int screenWidth;
    private int screenHeight;
    private Quaternion rotationQuat;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        //get the screen width and height to start off
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        //get the mouse input and compare to the screen width - edge offsett
        cameraRotate();

        rotationQuat = camera.transform.rotation;
    }
    private void cameraRotate()
    {
    if (Input.mousePosition.x > screenWidth - edgeOffset && rotationQuat.y < 10)
     {
        rotationQuat.y = Mathf.Clamp(rotationQuat.y + speed  * Time.deltaTime, -10,10);
        camera.transform.rotation = rotationQuat;
     }
     if (Input.mousePosition.x < 0 + edgeOffset && rotationQuat.y > -10)
     {
        rotationQuat.y = Mathf.Clamp(rotationQuat.y - speed  * Time.deltaTime, -10,10);
        camera.transform.rotation = rotationQuat;
     }
    //  if (Input.mousePosition.y > screenHeight - edgeOffset)
    //  {
    //     Mathf.Clamp(rotationVector.x -= speed  * Time.deltaTime, -10,10);
    //     rotationVector.z = 0;
    //     camera.transform.rotation = rotationVector;
    //  }
    //  if (Input.mousePosition.y < 0 + edgeOffset)
    //  {
    //     Mathf.Clamp(rotationVector.x += speed  * Time.deltaTime, -10,10);
    //     rotationVector.z = 0;
    //     camera.transform.rotation = rotationVector;
    //  }
    }
}
