using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlantPreference : MonoBehaviour
{
    public int companionScore = 0;
    public GameObject neighbourObject;
    public GameObject companionObject;
    public Color neighbourColor;

    private PlantBase plantBase;

    private List<Collider2D> hitCollidersList = new List<Collider2D>();
    
    private List<GameObject> neighboursList = new List<GameObject>();


    // Start is called before the first frame update
    void Awake ()
    {
        plantBase = GetComponent<PlantBase>();
    }

    private void Start()
    {
        //ProximityChecker();
    }

    // Update is called once per frame
    void Update()
    {
        ProximityChecker();
        PreferenceChecker();
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("hello");
        //Debug.Log("collision.gameObject: " + collision.gameObject.name);
        neighbourObject = collision.gameObject;
        neighbourColor = neighbourObject.GetComponent<PlantBase>().plantColor;
        PreferenceChecker();

        //neighboursList.Add(neighbourObject);
    }
    */

    private void ProximityChecker()
    {
        //GameObject overlappingObjects;
        //overlappingObjects = 
        //neighboursList.add = 
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        
        //Adjust radius based on circle collider radius*scale of object
        Physics2D.OverlapCircle(transform.position, 0.4f, filter, hitCollidersList);
        FilterNeighbours();
    }
    
    //Filter out your own colliders and only add a single version of each neighbour.
    private void FilterNeighbours()
    {
        neighboursList.Clear();
        foreach(Collider2D col in hitCollidersList)
        {
            //Not colliders on our gameObject this component is attached to
            //Only circle colliders 2D
            if(col.gameObject != gameObject && col.GetComponent<CircleCollider2D>() == true)
            {
                //Check for duplicates
                if(!neighboursList.Contains(col.gameObject))
                {
                    neighboursList.Add(col.gameObject);
                }
            }
        }
    }

    private void PreferenceChecker()
    {
        if(neighboursList.Count > 0)
        {
            //Check for companions
            foreach (GameObject neighbourGO in neighboursList)
            {
                if (plantBase.plantColor != neighbourGO.transform.gameObject.GetComponent<SpriteRenderer>().color)
                {
                    //Found a companion.
                    companionScore += 1;                    
                    companionObject = neighbourGO.transform.gameObject;
                    //neighbourColor = neighbourObject.GetComponent<PlantBase>().plantColor;
                    //break;
                }
                /*
                else
                {
                    neighbourObject = null;
                }
                */
            }
            //If no neighbour, then
            if(companionObject == null || !neighboursList.Contains(companionObject))
            {
                companionScore = 0;
            }
            
        }
        else 
        {
            //reset companionscore and companionObject?
            companionScore = 0;
            companionObject = null;
        }

        UpdateCompanionEffect();

        /*
        if(neighbourObject)
        {
            //Debug.Log("neighbourObject: " + neighbourObject);
            if (plantBase.plantColor != neighbourColor)
            {
                companionScore = 1;
                plantBase.companionEffect.SetActive(true);
                //GetComponent<SpriteRenderer>().size *= 2;
                //Vector2 plantScale = transform.lossyScale;
                //plantScale *= 2;
                //transform.lossyScale = plantScale;
            }
            
        }
        else if (neighbourObject == null)
        {
            companionScore = 0;
            plantBase.companionEffect.SetActive(false);
        }
        */
    }

    private void UpdateCompanionEffect()
    {
        if(companionScore > 0)
        {
            plantBase.companionEffect.SetActive(true);
        }
        else
        {
            plantBase.companionEffect.SetActive(false);
        }
    }
}
