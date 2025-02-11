using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int pickupCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Pickup")
        {
            pickupCount++;
            //Debug.Log("Pickup Count : " + pickupCount);
            Destroy(other.gameObject);
        }
    }
}
