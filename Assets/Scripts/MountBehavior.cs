using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountBehavior : MonoBehaviour
{
    //vTest1: Descreasing stamina over time that results in standstill.
    //vTest2: Check if we have plants in storage.
    //vTest3: Feed stamina if we have a plant in storage.
    //vTest4: Health influences stamina.
    //Test : No plant on the back = more hunger over time. Hunger decreases stamina. No stamina = can't move.
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    private int hunger;
    public float stamina;
    public float maxStamina;

    private float timeToDoStuff = 0;
    public Storage storage;
    public Motor_CharCtrl motor_CharCtrl;

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
            StaminaBehavior();
            timeToDoStuff = Time.time + 1.0f;
        }
    }
    private void StaminaBehavior()
    {
        
        stamina = stamina * (currentHealth/maxHealth);
        //Debug.Log("stamina: " + stamina);

        if (storage.storageCount > 0 && stamina < maxStamina)
        {
            stamina += 1;
            if(stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
        else if (stamina > 0)
        {
            stamina -= 1;
            if (stamina < 0)
            {
                stamina = 0;
            }
        }
        motor_CharCtrl.force = stamina;

        if (stamina <= 0)
        {
            Debug.Log("Can't move!");
            motor_CharCtrl.force = 0f;
        }
        Debug.Log("stamina: " + stamina);

    }
}
