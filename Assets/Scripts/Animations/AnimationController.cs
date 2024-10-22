using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Test1: Walk animation while moving foward.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0 || verticalInput > 0)
        {
            GetComponent<Animator>().SetTrigger("Walk");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Idle");
        }

    }
}
