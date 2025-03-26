using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject licht;
    public bool isOn = false;

    void Start()
    {
        //start met licht uit
        licht.SetActive(isOn);
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
                licht.SetActive(true);
                licht.GetComponent<Light>().enabled = true;
            }
            //turn light off
            else
            {
                licht.GetComponent<Light>().enabled = false;

                licht.SetActive(false);

            }
        }
    }
}

