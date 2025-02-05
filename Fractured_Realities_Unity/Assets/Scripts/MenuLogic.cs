using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject inventoryUI;
    public GameObject managers;
    public GameObject canvas;
    public GameObject menuUI;
    public GameObject healthUI;
    public GameObject toggleInventory;
    public GameObject hideInventory;
    public GameObject showInventory;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //GameObject inventoryUI = GameObject.FindGameObjectWithTag("Player");

        //GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        //GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        //GameObject menuUI = GameObject.FindGameObjectWithTag("MenuUI");
        //GameObject healthUI = GameObject.FindGameObjectWithTag("HealthUI");
        //GameObject hideInventory = GameObject.FindGameObjectWithTag("HideInventory");
        //GameObject showInventory = GameObject.FindGameObjectWithTag("ShowInventory");
        //GameObject toggleInventory = GameObject.FindGameObjectWithTag("ToggleInventory");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadScoreboard()
    {
        SceneManager.LoadScene("Scoreboard");
    }
    public void loadGame()
    {

        SceneManager.LoadScene("Map");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(managers);
        DontDestroyOnLoad(canvas);
        // DontDestroyOnLoad(Inventory);

        player.SetActive(true);
        managers.SetActive(true);
        canvas.SetActive(true);
        showInventory.SetActive(true);
        inventoryUI.SetActive(true);
        healthUI.SetActive(true);
        toggleInventory.SetActive(true);

        menuUI.SetActive(false);
        hideInventory.SetActive(true);

    }
}
