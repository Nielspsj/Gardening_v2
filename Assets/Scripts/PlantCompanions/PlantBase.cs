using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    public Color plantColor;
    public int plantNumber;
    public int plantSize;
    public GameObject companionEffect;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = plantColor;
    }
}
