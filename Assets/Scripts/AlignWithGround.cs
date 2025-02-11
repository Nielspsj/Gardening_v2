using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithGround : MonoBehaviour
{
    public LayerMask groundLayer;
    public float rotationSmoothing;

    private CharacterController body;
    private RaycastHit groundSurfaceHit;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        RayToGround();
        RotateToAlign();
    }

    private void RayToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5, groundLayer))
        {
            //Debug.Log("Hit the ground!");
            groundSurfaceHit = hit;
        }
    }
    private void RotateToAlign()
    {
        Quaternion newRotation = Quaternion.FromToRotation(transform.up, groundSurfaceHit.normal) * transform.rotation;
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, newRotation, Time.deltaTime * rotationSmoothing);
        //Debug.Log("Rotating");
    }
}
