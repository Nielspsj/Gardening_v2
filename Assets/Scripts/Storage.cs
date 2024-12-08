using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private List<Transform> storageObjectsList = new List<Transform>();
    public int storageCount
    {
        get { return storageObjectsList.Count; }
    }

    // Update is called once per frame
    void Update()
    {
        //WinState();
    }
    public void AddToStorage(Transform objToAdd)
    {
        storageObjectsList.Add(objToAdd);
    }
    public void RemoveFromStorage(Transform objToRemove)
    {
        storageObjectsList.Remove(objToRemove);
    }

    private void WinState()
    {
        if (storageObjectsList.Count > 5)
        {
            Debug.Log("You win!");
        }
    }
}
