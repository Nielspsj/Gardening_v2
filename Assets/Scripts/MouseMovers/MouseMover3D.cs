using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MouseMover3D : MonoBehaviour
{
    //vTest 1: Send ray with mouse and hit a collider. Get the collider name.
    //Doesn't ping super fast. Might need to make it ping more often. Maybe it's a mousedown thing?
    //It's because OnMouseButtonDown only runs once per click.
    //vTest 2: Only hit certain colliders tagged plant.
    //vTest 3: Disable collider of object tagged plant.
    //vTest 4: Move object tagged plant while holding the ray/mouse.
    //vTest 5: Get the length of the ray between origin and hit collider
    //Raycast.distance
    //vTest 6: Always move the tagged plant at the end of the ray slightly closer than the end of ray.
    //vCan't move currently since collider is disabled and will only work with the right tag.
    //vNow is positioned at hit.point.
    //vTest 7: Parent objectToMove under the storageObject.
    //vTest 8: Drop plant after moving it. 
    //vTest 9: Reset variables when moving should reset.
    //vTest 10: Sometimes plant isn't movable after moving off storage.
    //vTest 11: Not able to move or influence other plants when already holding a plant.

    private bool hitAPlant = false;
    private bool holdingAPlant = false;
    private bool hitTheStorage = false;
    private Transform objectToMove;
    private Transform storageObject;
    //public List<Transform> storageObjectsList = new List<Transform>();
    public Storage storage;

    private void Update () 
    {
        if (Input.GetMouseButtonUp(0))
        {   
            if (hitTheStorage == true && hitAPlant == true)
            {
                AttachToStorage();
            }
            else
            {
                DropPlant();
            }
            holdingAPlant = false;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If you push the left mouse button, send the ray
        if (Input.GetMouseButton(0))
        {
            SendRayFromMouse();
        }
              
    }

    private void SendRayFromMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //Something happens when it hits a plant or storage?

            //Hit any collider
            //Debug.Log("hit collder: " + hit.collider.gameObject.name);
            //Hit plant collider
            if (hit.collider.tag == "Plant" && holdingAPlant == false)
            {
                //Debug.Log("hit plant: " + hit.collider.gameObject.name);
                
                //Debug.DrawLine(ray.origin, hit.point);
                hitAPlant = true;
                holdingAPlant = true;

                objectToMove = hit.collider.transform;
                objectToMove.GetComponent<BoxCollider>().enabled = false;
                //Transform hitObject = hit.transform;
            }
            //We want the ray to continue casting even after hitting the plant and disabling the collider.
            if (hitAPlant == true)
            {
                MoveObject(hit);
            }

            if(hit.collider.tag == "Storage")
            {
                //Hit the storage object.
                
                hitTheStorage = true;
                storageObject = hit.collider.transform;
                //Debug.LogFormat("Hit the storage named: " + hit.collider.transform.name);
            }
            else
            {
                hitTheStorage = false;
                //storageObject = null;
            }

        }
    }

    //Disable collider of object and move it.
    private void MoveObject(RaycastHit hit)
    {
        //Debug.Log("name: " + hit.transform.name + " tag : " + hit.collider.tag + " - " + " hitPlant: " + hitAPlant);
        //Transform movedObject = hit.transform;
        if(holdingAPlant == true)
        {
            if (hit.collider.tag == "Plant")
            {

                
            }

            //movedObject.transform.position = Vector3.zero;
            //Vector3 rayLengthVector = hit.point - ray.origin;
            if (hitAPlant == true)
            {
                //objectToMove.parent = null;
                objectToMove.transform.position = hit.point;
            }
        }
             
    }
    
    //Parent moved object to the storage.
    private void AttachToStorage()
    {
        //Debug.Log("storage object info. " + "hitTheStorage: " + hitTheStorage + " - " + storageObject);
        objectToMove.parent = storageObject.transform;
        objectToMove.GetComponent<BoxCollider>().enabled = true;
        //storageObjectsList.Add(objectToMove);
        storage.AddToStorage(objectToMove);
        objectToMove = null;
        hitAPlant = false;
        hitTheStorage = false;
    }

    private void DropPlant()
    {
        if(objectToMove != null)
        {
            //Debug.Log("objectToMove: " + objectToMove);
            //objectToMove.parent = null;
            objectToMove.GetComponent<BoxCollider>().enabled = true;
            //storageObjectsList.Remove(objectToMove);
            storage.RemoveFromStorage(objectToMove);
            objectToMove = null;
            hitAPlant = false;
            hitTheStorage = false;
        }        
    }    
}
