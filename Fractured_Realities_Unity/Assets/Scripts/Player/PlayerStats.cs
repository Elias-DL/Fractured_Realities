using System;
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
    public bool Respawning = true;
    public GameObject JumpscareUI;
    public GameObject Canvas;
    private void Start() // of awake? idk voor nu voor in game time te zetten in DB
    {
        //Debug.Log("start");
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
        JumpscareUI = GameObject.FindWithTag("JumpscareUI");
        Canvas = GameObject.FindWithTag("Canvas");
    }


    private void Update()
    {
        StartSpawn = GameObject.FindWithTag("StartSpawn");
        if (player == null || healthBar == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (SceneManager.GetActiveScene().name != "Scoreboard" && SceneManager.GetActiveScene().name != "Main Menu")
        {
            //Debug.Log(SceneManager.GetActiveScene().name);
            time += Time.deltaTime;
            //Debug.Log("je speelt al " + time + " tijd");
        }

    }
    public void TakeDamage(float damage)
    {
        //Debug.Log(transform.position + " health : " + currentHealth);

        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0)
        {
            Respawn(StartSpawn);
            //Debug.Log("DEAD LOL LLLLLLLLLLLLLLL");
        }
    }


    public void Respawn(GameObject spawnPlek) // character controller spreekt teleportere tegen (effe uitzetten)
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            Respawning = true;
            
        }
        CharacterController CC = player.GetComponent<CharacterController>();
        CC.enabled = false;


        player.transform.position = spawnPlek.transform.position;
        player.transform.rotation = spawnPlek.transform.rotation;
        //Debug.Log("Respawned at " + player.transform.position + " health : " + currentHealth);

        CC.enabled = true;
        currentHealth = 100;
        healthBar.SetSlider(currentHealth);
        deaths++;
        //Debug.Log("Death(s):" + deaths);
        //JumpscareUI.SetActive(false);

    }


    public void Escape()

    {
        escaped = true;
        Destroy(player);
        Destroy(Canvas);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Scoreboard");
    }
    
}
