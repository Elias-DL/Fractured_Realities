using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;


    public GameObject player;
    public GameObject StartSpawn;   
    public HealthBar healthBar;
    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }
    private void Update()
    {
        StartSpawn = GameObject.FindWithTag("StartSpawn");

    }
    public void TakeDamage(float damage)
    {
        Debug.Log(transform.position + " health : " + currentHealth);

        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0) 
        {
            Respawn();
            Debug.Log("DEAD LOL LLLLLLLLLLLLLLL");
        }
    }


    public void Respawn() // hoe tf werkt dit niet _> character controller spreekt teleportere tegen (effe uitzetten)
    {

        CharacterController CC = player.GetComponent<CharacterController>();
        CC.enabled = false;


        transform.position = StartSpawn.transform.position;
        Debug.Log("Respawnded at " + player.transform.position + " health : " + currentHealth);

        CC.enabled = true;
        currentHealth = 100;
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
