using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlantPreference3D : MonoBehaviour
{
    //vTest 11: Companion behavior transfered from 2D to 3D.
    //Might need to adjust handling of the neighbour gathering since swapping to Array from List.
    //vTest 12: Companion behavior working on the horse storage. For effect use colored particle effect.
    //vTest 13: Clean the script so it is more readable at a glance.
    //TEst 14: Optimize: Perhaps only do proximity check when we move and place the plants.
    //Test 15: Create a companion list of colors instead of any other color and dislikes?
    //

    public int companionScore = 0;
    //public GameObject neighbourObject;
    private GameObject companionObject;
    //public Color neighbourColor;

    private PlantBase plantBase;

    //private List<Collider2D> hitCollidersList = new List<Collider2D>();
    private Collider[] hitCollidersArray;
    public List<GameObject> neighboursList = new List<GameObject>();
    //public List<Color> dislikesList = new List<Color>();
    public List<Color> likesList = new List<Color>();
    public Color dislikeColor = Color.blue;


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
   
    private void ProximityChecker()
    {
        //ContactFilter2D filter = new ContactFilter2D();
        //filter.NoFilter();

        //Adjust radius based on sphere collider radius*scale of object.
        //OLD: Physics2D.OverlapCircle(transform.position, 0.4f, filter, hitCollidersList);
        hitCollidersArray = Physics.OverlapSphere(transform.position, 0.7f);
        FilterNeighbours();
    }
    
    //Filter out your own colliders and only add a single version of each neighbour.
    private void FilterNeighbours()
    {
        neighboursList.Clear();
        foreach(Collider col in hitCollidersArray)
        {
            //Not colliders on our gameObject this component is attached to
            //Only box colliders
            if(col.gameObject != gameObject && col.GetComponent<BoxCollider>() == true && col.gameObject.tag == "Plant")
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
                foreach(Color likesColor in likesList)
                {
                    if(likesColor == neighbourGO.transform.gameObject.GetComponent<PlantBase>().plantColor && dislikeColor != neighbourGO.transform.gameObject.GetComponent<PlantBase>().plantColor)
                    {
                        //Found a companion.
                        companionScore += 1;
                        companionObject = neighbourGO.transform.gameObject;
                    }
                    else
                    {
                        //Something extra?
                    }
                }                
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
