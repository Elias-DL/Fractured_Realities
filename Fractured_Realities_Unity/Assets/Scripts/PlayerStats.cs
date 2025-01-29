using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public HealthBar healthBar;
    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);
    }


    //private void Update() manueel testen
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {

    //        TakeDamage(20f);

    //    }
    //}
}
