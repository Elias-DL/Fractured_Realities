using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PaintingCube[] paintings; // Assign all PaintingCubes in Inspector
    public GameObject door; // Assign the door GameObject

    private void Awake()
    {
        Instance = this;
    }

    public void CheckPaintings()
    {
        foreach (PaintingCube painting in paintings)
        {
            if (!painting.gameObject.activeSelf || !painting.gameObject.GetComponent<PaintingCube>().enabled)
            {
                Debug.Log("Not all paintings are placed yet.");
                return; // Exit if any painting is missing
            }
        }

        OpenDoor(); // If all paintings are placed, open the door
    }

    private void OpenDoor()
    {
        Debug.Log("All paintings are placed! Opening the door...");
        door.SetActive(true); // Activate the door (or replace with an animation)
    }
}
