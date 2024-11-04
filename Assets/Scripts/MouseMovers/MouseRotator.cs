using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    public float sensitivity = 2f;

    private float x;
    private float y;
    private Vector3 rotate;

    private void Awake()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        x = euler.x;
        y = euler.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWithMouseInput();
    }

    //Grab mouse input and rotate the transform this script is on.
    private void RotateWithMouseInput()
    {
        float MIN_Y = 0.0f;
        float MAX_Y = 360.0f;
        float MIN_X = -45.0f;
        float MAX_X = 90.0f;

        y += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
        if (y < MIN_Y)
        {
            y += MAX_Y;
        }
        else if (y > MAX_Y)
        {
            y -= MAX_Y;
        }
        x -= Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
        if (x < MIN_X)
        {
            x = MIN_X;
        }
        else if(x > MAX_X)
        {
            x = MAX_X;
        }

        transform.rotation = Quaternion.Euler(x, y, 0);
        //rotate = new Vector3(x, y, 0);
        //transform.eulerAngles = transform.eulerAngles - rotate;
        //transform.Rotate(rotate);

    }
}
