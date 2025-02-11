using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense_Smell : MonoBehaviour
{
    //vTest1: Sphere raycast around that detects plants.
    //Test1.5: Add to list to check which is closest
    //vTest2: Determine closest food in the Array
    //Test3: Disable as food if in storage


    public Collider[] hitCollidersArray;
    public LayerMask foodLayerMask;
    //private List<GameObject> foodList = new List<GameObject>(); //Maybe use if Arrays are too annoying to use
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProximityChecker();
        FindClosestTarget();
    }   

    private void ProximityChecker()
    {
        //Adds and takes away from Array based on the overlaping sphere.
        hitCollidersArray = Physics.OverlapSphere(transform.position, 10f, foodLayerMask);
    }

    private void FindClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistanceSqr = 25000f;
        Vector3 currentPosition = transform.position;
        if (hitCollidersArray.Length > 0)
        {
            foreach (Collider collider in hitCollidersArray)
            {
                Vector3 directionToTarget = collider.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestTarget = collider.transform;
                    //Debug.Log("closestTarget: " + closestTarget);
                }
            }
        }        
    }
}
