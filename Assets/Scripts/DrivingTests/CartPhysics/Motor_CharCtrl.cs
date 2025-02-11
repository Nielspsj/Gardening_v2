using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor_CharCtrl : MonoBehaviour
{
    //Test1: Set target for independent mound movement. Random at first
    //Test2: Make it possible to set movementTarget from Behavior?
    //Test3: Navmesh??
   
    [Tooltip("Maximum force to get this motor running")]
    public float force = 800;
    public float maxSpeed = 1f;
    [Tooltip("Turn this amount to the right every 1/50 sec.")]
    public float rotationSpeed = 1f;
    
    private CharacterController body;
    private float horizontalInput;
    private float verticalInput;
    private float gravity = 5.5f;
    private Vector3 bodyVelocity;

    private Vector3 movementTarget;

    // Start is called before the first frame update
    public void Start() {
        body = GetComponent<CharacterController>();
    }    
   
    private void Update()
    {
        ControlHorse();        
    }

    private void ControlHorse()
    {
        //Gracity
        if (body.isGrounded == true)
        {
            bodyVelocity.y = 0;
        }
        else
        {
            bodyVelocity.y = -gravity * Time.deltaTime;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log("horizontalInput: " + horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        
        body.transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * (10f * Time.deltaTime));
        Vector3 movement = verticalInput * body.transform.forward;

        //Move + gravity
        body.Move(movement * force * Time.deltaTime);
        body.Move(bodyVelocity);
    }
    
    private void MountMovement()
    {
        //Random rotation for direction
    }
}
