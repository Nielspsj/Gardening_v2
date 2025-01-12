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
    private float currentStamina;
    public float maxStamina;
    public float staminaGainPSecond = 1;
    public float staminaLossPSecond = 1;


    private float timeToDoStuff = 0;
    public Storage storage;
    public Motor_CharCtrl motor_CharCtrl;

    // Start is called before the first frame update
    void Start()
    {
        //Storage storage = new Storage();
        Debug.Log("storagecount: " + storage.storageCount);
        currentStamina = maxStamina;
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

        //currentStamina = currentStamina * (currentHealth/maxHealth);
        //Debug.Log("stamina: " + stamina);

        if (storage.storageCount > 0 && currentStamina < maxStamina)
        {
            currentStamina += staminaGainPSecond;
            if(currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
        else if (currentStamina > 0)
        {
            currentStamina -= staminaLossPSecond;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }
        //Decrease movement the less stamina the mount has.
        //motor_CharCtrl.force = motor_CharCtrl.force * (currentStamina/100);

        if (currentStamina <= 0)
        {
            Debug.Log("Can't move!");
            motor_CharCtrl.force = 0f;
        }
        //Debug.Log("storage.storageCount: " + storage.storageCount);
        Debug.Log("currentStamina: " + currentStamina);

    }
}
