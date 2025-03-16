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

    public void SetSliderMax(float value)
    {
        healthSlider.maxValue = value;
        SetSlider(value);
    }
}
