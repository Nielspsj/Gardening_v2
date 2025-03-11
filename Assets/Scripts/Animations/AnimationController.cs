using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Test1: Walk animation while moving foward.

    private float verticalInput = 0;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //DirectControl();
        IndirectControl();        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Scream");
        }

    }
    private void DirectControl()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0 || verticalInput > 0)
        {
            GetComponent<Animator>().SetTrigger("Walk");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
    private void IndirectControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            verticalInput = 1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            verticalInput = 0f;
        }
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
