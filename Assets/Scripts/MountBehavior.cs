using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountBehavior : MonoBehaviour
{
    //vTest1: Descreasing stamina over time that results in standstill.
    //vTest2: Check if we have plants in storage.
    //Test3: Feed stamina if we have a plant in storage.
    //Test : No plant on the back = more hunger over time. Hunger decreases stamina. No stamina = can't move.
    public int maxHealth;
    public int currentHealth;
    public int hunter;
    public float stamina = 100;
    private float timeToDoStuff = 0;
    public Storage storage;
    // Start is called before the first frame update
    void Start()
    {
        //Storage storage = new Storage();
        Debug.Log("storagecount: " + storage.storageCount);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if(Time.time >= timeToDoStuff)
        {
            stamina -= 1;
            //Debug.Log("stamina: " + stamina);
            Debug.Log("storagecount: " + storage.storageCount);

            if (stamina <= 0)
            {
                Debug.Log("Can't move!");
            }
            timeToDoStuff = Time.time + 1.0f;
        }
    }
    private void StaminaBehavior()
    {
        if (storage.storageCount > 0)
        {
            stamina += 1;
        }
        else
        { 

        }
    }
}
