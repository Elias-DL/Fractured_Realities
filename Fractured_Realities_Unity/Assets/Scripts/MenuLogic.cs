
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject Player;
    public GameObject ItemCanvas;
    public GameObject Inventory;
    public GameObject Managers;
    public GameObject Canvas;
    public GameObject Menu;
    public GameObject HealthStats;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //GameObject ItemCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        //GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
        //GameObject Managers = GameObject.FindGameObjectWithTag("Managers");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadGame()
    {

        SceneManager.LoadScene("Map");
        DontDestroyOnLoad(Managers);
        DontDestroyOnLoad(Canvas);
        DontDestroyOnLoad(Player);
        // DontDestroyOnLoad(Inventory);

        Managers.SetActive(true);
        Player.SetActive(true);
        ItemCanvas.SetActive(true);
        Inventory.SetActive(true);
        Canvas.SetActive(true);
        Menu.SetActive(false);
        HealthStats.SetActive(true);
    }
}
