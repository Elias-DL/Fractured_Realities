using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private float currentHealth;
    public GameObject player;

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

        if (currentHealth == 0)
        {
            Respawn();
            Debug.Log("DEAD LOL LLLLLLLLLLLLLLL");
        }
    }


    public void Respawn() // hoe tf werkt dit niet 
    {
        Vector3 RespawnPOS = GameObject.FindWithTag("StartSpawn").transform.position;
        player.transform.position = RespawnPOS;   
        Debug.Log("RESPAWNED at " + RespawnPOS + " health : " + currentHealth);
    }

    //private void Update() manueel testen
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {

    //        TakeDamage(20f);

    //    }
    //}
}
