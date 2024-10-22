using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseMover3D : MonoBehaviour
{
    Vector3 mousePosition;    

    //Grab transform position on the screen
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
}
