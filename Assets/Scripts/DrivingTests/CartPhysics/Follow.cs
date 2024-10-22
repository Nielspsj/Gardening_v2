using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target_TF;
    private float yRotation_F;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target_TF.position + new Vector3(0,0,-2);
        //transform.rotation = target_TF.rotation;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target_TF.position + new Vector3(0, -0.5f, -1);
        //transform.position = target_TF.position;
        //transform.rotation = target_TF.rotation;
        yRotation_F = target_TF.rotation.y;
        Debug.Log("yRotation_F: " + yRotation_F);
        transform.rotation = Quaternion.Euler(0, yRotation_F*100, 0);
    }
}
