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
        if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "Anklegrabber")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureAnklegrabber);
        }

        else if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "ZombieWithBlood")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureZombieWithBlood);

        }

        else if (Input.GetKeyDown(KeyCode.F) && Player.GetComponent<PlayerMovement>().naamGezien == "Mutated")
        {
            Managers.GetComponent<InventoryManager>().Items.Add(pictureMutated);

        }
    }
}
