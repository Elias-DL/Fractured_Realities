using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject inventoryUI;
    public GameObject managers;
    public GameObject canvas;
    public GameObject IngameMenu;
    public GameObject menuUI;
    public GameObject healthUI;
    public GameObject toggleInventory;
    public GameObject hideInventory;
    public GameObject showInventory;
    public GameObject LoadingScreen;
    public GameObject JumpscareUI;
    public GameObject TipsUI;
    public GameObject guessingGameUI;
    // Start is called before the first frame update
    void Start() // via inspector is makkelijker en betrouwbaarder -> minder snel fouten
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //inventoryUI = GameObject.FindGameObjectWithTag("Player");
        //managers = GameObject.FindGameObjectWithTag("Managers");
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
        //menuUI = GameObject.FindGameObjectWithTag("MenuUI");
        //healthUI = GameObject.FindGameObjectWithTag("HealthUI");
        //hideInventory = GameObject.FindGameObjectWithTag("HideInventory");
        //showInventory = GameObject.FindGameObjectWithTag("ShowInventory");
        //toggleInventory = GameObject.FindGameObjectWithTag("ToggleInventory");
        //IngameMenu = GameObject.FindWithTag("InGameMenu");
        //LoadingScreen = GameObject.FindWithTag("LoadingScreen");
        //Jumpscare = GameObject.FindWithTag("Jumpscare");
        //TipsUI = GameObject.FindWithTag("TipsUI");
        //guessingGameUI = GameObject.FindWithTag("guessingGameUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // Een "menu" opent wanneer je op escape drukt zodat je het spel kan afsluiten
        {


            if (IngameMenu.activeSelf == true)
            {
                IngameMenu.SetActive(false);
            }
            else
            {
                IngameMenu.SetActive(true);
            }
        }
    }

    public void loadScoreboard()
    {

        DontDestroyOnLoad(managers);
        SceneManager.LoadScene("Scoreboard");
    }
    public void loadGame()
    {

        SceneManager.LoadScene("Map");

        menuUI.SetActive(false);

        LoadingScreen.SetActive(true);
        StartCoroutine(StartGame());

        DontDestroyOnLoad(managers);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);
        TipsUI.SetActive(true);
        player.SetActive(true);
        managers.SetActive(true);
        canvas.SetActive(true);
        showInventory.SetActive(true);
        inventoryUI.SetActive(true);
        healthUI.SetActive(true);
        toggleInventory.SetActive(true);
        hideInventory.SetActive(true);
        JumpscareUI.SetActive(true);
        JumpscareUI.SetActive(false);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() // Het spel volledig afsluiten
    {
        Application.Quit();
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        menuUI.SetActive(false);
        LoadingScreen.SetActive(false);


    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("LogIn");
    }

}
