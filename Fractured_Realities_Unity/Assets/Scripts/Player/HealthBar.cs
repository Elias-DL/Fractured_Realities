using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;


    public void SetSlider(float value) 
    {
        healthSlider.value = value;
    }

    public void SetSliderMax(float value) // het maximum aantal health dat je kan hebben instellen met parameter
    {
        healthSlider.maxValue = value;
        SetSlider(value);
    }
}
