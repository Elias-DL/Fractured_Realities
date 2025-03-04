using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    public GameObject player;
    public GameObject StartSpawn;
    public HealthBar healthBar;
    public int deaths;
    public float time;
    public bool escaped = false;
    private void Start() // of awake? idk voor nu voor in game time te zetten in DB
    {
        Debug.Log("start");
        currentHealth = maxHealth;
       
        healthBar.SetSliderMax(maxHealth);

    }


    private void Update()
    {
        StartSpawn = GameObject.FindWithTag("StartSpawn");

        if (SceneManager.GetActiveScene().name != "Scoreboard" && SceneManager.GetActiveScene().name != "Main Menu")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            time += Time.deltaTime;
            //Debug.Log("je speelt al " + time + " tijd");
        }

    }
    public void TakeDamage(float damage)
    {
        Debug.Log(transform.position + " health : " + currentHealth);

        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0)
        {
            Respawn(StartSpawn);
            Debug.Log("DEAD LOL LLLLLLLLLLLLLLL");
        }
    }


    public void Respawn(GameObject spawnPlek) // hoe tf werkt dit niet _> character controller spreekt teleportere tegen (effe uitzetten)
    {

        CharacterController CC = player.GetComponent<CharacterController>();
        CC.enabled = false;


        player.transform.position = spawnPlek.transform.position;
        player.transform.rotation = spawnPlek.transform.rotation;
        Debug.Log("Respawned at " + player.transform.position + " health : " + currentHealth);

        CC.enabled = true;
        currentHealth = 100;
        healthBar.SetSlider(currentHealth);
        deaths++;
        Debug.Log("Death(s):" + deaths);
    }


    public void Escape()

    {
        escaped = true;
        Debug.Log("je bent escaped enzo");
    }
    //private void Update() manueel testen
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {

    //        TakeDamage(20f);

    //    }
    //}
}
