using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMonsters : MonoBehaviour
{

    public GameObject Image; // component in de inspector
    public string imgMonster; // naam van het monster
    public GameObject GameObjectMonster;
        // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(imgMonster + "Gevonden"))
        {
            Image.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (EquippedItemManager.Instance.EquippedItemName == imgMonster )
        {
            Image.SetActive(true);
            GameObjectMonster = GameObject.FindWithTag("Image"+imgMonster);
            Destroy(GameObjectMonster);
            EquippedItemManager.Instance.ClearEquippedItem();
            Debug.Log(imgMonster);
            PlayerPrefs.SetString(imgMonster + "Gevonden", "true");
            PlayerPrefs.Save();
        }
    }
}
