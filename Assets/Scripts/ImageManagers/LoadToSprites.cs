using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToSprites : MonoBehaviour
{
    //vTest 1: Grab all Image objects that should get a new Sprite to their renderer. By tag imageForPhoto at Start. Wait for ImageCollector
    //vTest 2: Add texture2D to the sprite renderer of the grabbed images.
    //vTest 3: Have all sprites fit the same uniform scale for ingame image objects. All photos are scaled using pixels per inch now.

    private ImageCollector imageCollector;
    public GameObject[] imagesArray;
    public List<GameObject> imagesForPhoto = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        imageCollector = GetComponent<ImageCollector>();
        //FindAndCollectImagesForPhotos();
        StartCoroutine(Delay());
    }

    //Collect the image objects and put them into a list.
    private void FindAndCollectImagesForPhotos()
    {
        imagesArray = GameObject.FindGameObjectsWithTag("ImageForPhoto");

        foreach (GameObject image in imagesArray)
        {
            imagesForPhoto.Add(image);
        }

        AddPhotoToSpriteRenderer();
    }

    //Add the collected photos to the sprite renderer of the collected image objects.
    private void AddPhotoToSpriteRenderer()
    {
        for(int i = 0; i < imagesForPhoto.Count; i++)
        {
            Debug.Log("i: " + i);
            imagesForPhoto[i].GetComponent<SpriteRenderer>().sprite = imageCollector.photosList[i];
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        FindAndCollectImagesForPhotos();
    }
}
