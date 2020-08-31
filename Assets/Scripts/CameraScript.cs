using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed = 30f;
    private int edgeOffset = 30;
    private int screenWidth;
    private int screenHeight;
    private Vector3 rotationVec;
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
        rotationVec = camera.transform.eulerAngles;

        //get the mouse input and compare to the screen width - edge offsett
        cameraRotate();
    }
    private void cameraRotate()
    {
        if (Input.mousePosition.x > screenWidth - edgeOffset)
        {
            // Debug.Log("Going Right");
            rotationVec.y += speed * Time.deltaTime;
            if(rotationVec.y >30 && rotationVec.y < 180)
            rotationVec.y = 30;
        }
        else if (Input.mousePosition.x < 0 + edgeOffset)
        {
            // Debug.Log("Going Left");
            rotationVec.y -= speed * Time.deltaTime;
            if(rotationVec.y < 330 && rotationVec.y > 180)
            rotationVec.y = 330;
        }
        //Debug.Log("Pre:" + rotationVec.y);
        //rotationVec.y = Mathf.Clamp(rotationVec.y, (float)(-30),(float)(30));
        //Debug.Log("Post:" + rotationVec.y);
        camera.transform.eulerAngles = rotationVec;
    }
}
