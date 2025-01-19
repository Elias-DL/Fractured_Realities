using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject Player;
    public GameObject ItemCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadGame()
    {

        SceneManager.LoadScene("Map");
        DontDestroyOnLoad(ItemCanvas);
        DontDestroyOnLoad(Player);

        Player.SetActive(true);
        ItemCanvas.SetActive(true);
    }
}
