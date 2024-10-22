using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCollector : MonoBehaviour
{
    //vTest 1: Gather images from the Resources/Photos folder into a list
    //Test 2: Add new images to the image list at runtime?
    //Test 3: Add ONLY new images to the image list

    //public Object testSprite;
    //Every object in the folder put into an Array
    private Object[] photosArray;
    //A list with only the sprites from the Array
    public List<Sprite> photosList = new List<Sprite>();

    // Start is called before the first frame update
    void Awake()
    {
        CollectFromFolder();
    }

    //Load from folder to Array
    private void CollectFromFolder()
    {
        photosArray = Resources.LoadAll("Photos");
        CastToTypeIntoList();
    }

    private void CastToTypeIntoList()
    {
        foreach (Object photoSprite in photosArray)
        {
            //Debug.Log("photo: " + photoSprite.GetType());

            if (photoSprite.GetType() == typeof(Sprite))
            {
                photosList.Add(photoSprite as Sprite);

            }
        }
        /*
        foreach (Sprite photoSprite in photosList)
        {
            Debug.Log("photoSprite name: " + photoSprite.name);
        }
        */
    }
}
