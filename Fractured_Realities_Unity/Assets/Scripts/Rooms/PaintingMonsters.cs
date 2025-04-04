using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMonsters : MonoBehaviour
{

    public GameObject Image; // component in de inspector
    public string imgMonster; // naam van het monster
    public GameObject GameObjectMonster;
        
    void Start()
    {
        if (PlayerPrefs.HasKey(imgMonster + "Gevonden")) // bijhouden of het schilderij al is is opgehangen
                                                         // zodat als je de kamer kan verlaten en niet opnieuw moet beginnen
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
        if (EquippedItemManager.Instance.EquippedItemName == imgMonster ) // als het item van het juiste monster equipped is komt de image tevoorschijn
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
