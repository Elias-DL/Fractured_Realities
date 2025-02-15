using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; 
    private bool isOn = false; 

    void Start()
    {
        //start met licht uit
        lightGO.SetActive(isOn);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            //toggle light
            isOn = !isOn;
            //turn light on
            if (isOn)
            {
                lightGO.SetActive(true);
            }
            //turn light off
            else
            {
                lightGO.SetActive(false);

            }
        }
    }
}
