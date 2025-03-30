using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{

    public GameObject Player;
    public Item pictureAnklegrabber;
    public Item pictureZombieWithBlood;
    public Item pictureMutated;
    public GameObject Managers;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Managers = GameObject.FindWithTag("Managers");
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "AnkleGrabber")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureAnklegrabber);
            Debug.Log("anklegrabber stalked");
            InventoryManager.Instance.ListItems();// update de ui direct 
        }

        else if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "Bookhead")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureZombieWithBlood);
            Debug.Log("bookhead stalked");
            InventoryManager.Instance.ListItems();
        }

        else if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "Zombie")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureMutated);
            Debug.Log("zombie stalked");
            InventoryManager.Instance.ListItems();
        }
    }
}
