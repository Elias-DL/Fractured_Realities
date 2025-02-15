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


        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
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


    public void Respawn() // hoe tf werkt dit niet 
    {
        

        Vector3 RespawnLocation = StartSpawn.transform.position;
        player.GetComponent<Rigidbody>().MovePosition(RespawnLocation);
        Debug.Log(RespawnLocation + " health : " + currentHealth);


        //currentHealth = 100;
        //healthBar.SetSlider(currentHealth);
    }

    //private void Update() manueel testen
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {

    //        TakeDamage(20f);

    //    }
    //}
}
