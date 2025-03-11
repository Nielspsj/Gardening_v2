using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    //Test1: Add to RotateWithMouseInput the current rotation.
    public float sensitivity = 2f;

    private float x;
    private float y;
    private Vector3 rotate;

    private float timeCount = 0.0f;

    private void Awake()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        x = euler.x;
        y = euler.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Need to start at current rotation of the camera holder.
            //y = transform.localEulerAngles.y;
            //x = transform.localEulerAngles.x;
        }
        
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            RotateWithMouseInput();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Grab mouse input and rotate the transform this script is on.
    private void RotateWithMouseInput()
    {
        float min_y = 0.0f;
        float max_y = 360.0f;
        float min_x = -45.0f;
        float max_x = 90.0f;

        

        y += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
        if (y < min_y)
        {
            y += max_y;
        }
        else if (y > max_y)
        {
            y -= max_y;
        }
        x -= Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
        if (x < min_x)
        {
            x = min_x;
        }
        else if(x > max_x)
        {
            x = max_x;
        }

        //transform.rotation = Quaternion.Euler(x, y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x, y, 0), 1000);
        //timeCount = timeCount + Time.deltaTime;
        //rotate = new Vector3(x, y, 0);
        //transform.eulerAngles = transform.eulerAngles - rotate;
        //transform.Rotate(rotate);

    }
}
