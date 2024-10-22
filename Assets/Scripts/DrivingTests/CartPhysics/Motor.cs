using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public bool motorOn = false;
    [Tooltip("Maximum force to get this motor running")]
    public float force = 800;
    public float maxSpeed = 1f;
    [Tooltip("Turn this amount to the right every 1/50 sec.")]
    public float rotationSpeed = 1f;
    [Tooltip("The rotation speed when we are standing still")]
    public float standstillRotationSpeed = 100f;
    //[Tooltip("Run an interval of start/stop for this number of seconds")]
    //public float runInterval = 15;

    private Rigidbody body;
    private float forceFactor = 0f;
    //float rotationSpeedAdjustment;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    public void Start() {
        body = GetComponent<Rigidbody>();
        //rotationSpeedAdjustment = horizontalInput / 1000f;
    }    

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        //Turn off motor after a certain time/runInterval.
        if (runInterval > 0) 
        {
            motorOn = (Mathf.Sin(Time.fixedTime * 2 * Mathf.PI / runInterval)) > -0.1f;
        }
        */

        if (motorOn) 
        {
            float speed = body.velocity.magnitude;
            if (forceFactor < 1f) 
            {
                forceFactor += 0.5f;
            }
            forceFactor += 0.5f;
            body.AddRelativeForce(Vector3.Lerp(Vector3.zero, Vector3.forward * force * forceFactor * verticalInput, 1f - speed/maxSpeed));
            transform.rotation *= Quaternion.AngleAxis(90 * horizontalInput/100 * speed * rotationSpeed, Vector3.up);
            body.rotation = transform.rotation;
        } else 
        {
            forceFactor = 0.1f;
        }
    }

    private void Update()
    {
        ControlHorse();
        Debug.Log("motor: " + motorOn);
    }

    private void ControlHorse()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log("horizontalInput: " + horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        //Debug.Log("verticalInput: " + verticalInput);
        if(verticalInput > 0)
        {
            motorOn = true;
        }
        else if(verticalInput < 0)
        {
            //verticalInput *= 0.2f;
            motorOn = true;
        }
        else
        {
            motorOn = false;
        }

        //Rotating in place NEEDS TO INCLUDE THE RIGHT FORCE FROM WHEN THE MOTOR IS ON
        if(horizontalInput > 0 && motorOn == false)
        {
            transform.rotation *= Quaternion.AngleAxis(90 * (horizontalInput / 1000) * standstillRotationSpeed, Vector3.up);
            body.rotation = transform.rotation;
        }
        if (horizontalInput < 0 && motorOn == false)
        {
            transform.rotation *= Quaternion.AngleAxis(90 * (horizontalInput / 1000) * standstillRotationSpeed, Vector3.up);
            body.rotation = transform.rotation;
        }

    }
}
