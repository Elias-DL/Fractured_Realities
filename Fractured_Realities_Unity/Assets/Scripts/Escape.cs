using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject Managers;
    public DBConnection dbConnection;  

    int totalDeaths;
    float escapeTime;
    bool escaped;
    string targetScene = "Scoreboard";
    // Start is called before the first frame update
    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");

        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {

        escaped = Managers.GetComponent<PlayerStats>().escaped;

        //totalDeaths = Managers.GetComponent<PlayerStats>().deaths;
        //escapeTime = Managers.GetComponent<PlayerStats>().time;
        //Debug.Log("total deaths " + totalDeaths + " escape time = " + escapeTime); 
        
        SceneManager.LoadScene(targetScene);
        //dbConnection = GameObject.FindWithTag("DBConnection").GetComponent<DBConnection>();

        //LoadScene();
    }


    //public void LoadScene()
    //{

    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    GameObject inventoryUI = GameObject.FindGameObjectWithTag("Player");

    //    GameObject managers = GameObject.FindGameObjectWithTag("Managers");
    //    GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
    //    //GameObject menuUI = GameObject.FindGameObjectWithTag("MenuUI");
    //    GameObject healthUI = GameObject.FindGameObjectWithTag("HealthUI");
    //    GameObject hideInventory = GameObject.FindGameObjectWithTag("HideInventory");
    //    GameObject showInventory = GameObject.FindGameObjectWithTag("ShowInventory");
    //    GameObject toggleInventory = GameObject.FindGameObjectWithTag("ToggleInventory");



    //    DontDestroyOnLoad(managers);
    //    DontDestroyOnLoad(canvas);
    //    DontDestroyOnLoad(player);


    //    player.SetActive(true);
    //    managers.SetActive(true);
    //    canvas.SetActive(true);
    //    showInventory.SetActive(true);
    //    inventoryUI.SetActive(true);
    //    //healthUI.SetActive(true);
    //    toggleInventory.SetActive(true);

    //    //menuUI.SetActive(false);
    //    hideInventory.SetActive(false);
    //}
}
