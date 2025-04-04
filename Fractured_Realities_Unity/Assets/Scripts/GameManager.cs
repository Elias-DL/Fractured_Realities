using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Key;
    public GameObject imgBoat;
    public GameObject imgWoman;
    public GameObject imgEarth;
    public GameObject imgLandscape;
    public GameObject imgWolf;
    public GameObject imgBridge;
    public GameObject Key6;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (imgBoat.activeSelf == true && imgWoman.activeSelf == true && imgEarth.activeSelf == true && imgLandscape == true && imgWolf.activeSelf == true && imgBridge.activeSelf == true)
        {

            if (Key != null)
            {
                ShowKey();
                Debug.Log("key");
            }
            
        }
    }
    
       
  

    private void ShowKey()
    {
        Debug.Log("All paintings are placed!");
        Key6.SetActive(true); // Activate the door (or replace with an animation)
    }
}
