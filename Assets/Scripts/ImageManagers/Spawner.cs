using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ImageCollector imageCollector;
    private int spawnAmount = 0;
    public List<GameObject> spawnList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("photos: " + imageCollector.photosList.Count);
        StartCoroutine(spawnDelay());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(0);
        spawnAmount = imageCollector.photosList.Count;
        Debug.Log("amount: " + spawnAmount);
        SpawnFromList();
    }

    private void SpawnFromList()
    {
        for(int i = 0; i < imageCollector.photosList.Count; i++)
        {
            int randomNr = Random.Range(0, 1);
            Vector3 spawnPos = new Vector3(Random.Range(2, 50),0.5f, Random.Range(2, 50));
            GameObject gnomeObj = Instantiate(spawnList[randomNr].gameObject, spawnPos, spawnList[randomNr].gameObject.transform.rotation);
            gnomeObj.transform.parent = transform;
            //gnomeObj.transform.tag = "ImageForPhoto";
        }
    }
}
