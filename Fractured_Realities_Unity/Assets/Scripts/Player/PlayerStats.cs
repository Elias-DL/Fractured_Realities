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
    //public GameObject JumpscareUI; // Uitbreiding
    public GameObject Canvas;
    public GameObject DiedUI;
    private void Start() 
    {
        //Debug.Log("start");
        currentHealth = maxHealth;
        if (DiedUI == null)
        {
            Debug.LogWarning("DiedUI is NOT assigned!");
        }
        healthBar.SetSliderMax(maxHealth);
        //JumpscareUI = GameObject.FindWithTag("JumpscareUI");
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
    public void TakeDamage(float damage) // Als een enemy damage doet wordt dit het health niveau aangepast
    {
        //Debug.Log(transform.position + " health : " + currentHealth);

        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0)
        {
            Respawn(StartSpawn);
            //Debug.Log("DEAD");
        }
    }


    public void Respawn(GameObject spawnPlek) // character controller spreekt teleportere tegen (even uitzetten)
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            Respawning = true;

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
            StartCoroutine(Died()); // nadat de speler en camera zijn gereset

        }

        else
        {
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
            DiedUI.SetActive(false);

        }

    }


    public void Escape()

    {
        escaped = true;
        Destroy(player);
        Destroy(Canvas);
        Cursor.lockState = CursorLockMode.Confined; // cursor vrij laten voor interactie met scorebord mogelijk te maken
        SceneManager.LoadScene("Scoreboard");
        Destroy(player);
    }

    IEnumerator Died()
    {

        DiedUI.SetActive(true); // laten weten aan de speler dat je bent doodgegaan, aangezien de teleportatie een beetje abrupt gebeurt
        
        yield return new WaitForSeconds(2f);
        Debug.Log("dead");

        DiedUI.SetActive(false);


    }

}
