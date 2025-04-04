using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingVaria : MonoBehaviour
{
    public GameObject Painting; // Image compontent in de inspector
    public string imagePainting; // Naam van de painting
    public GameObject GameObjectPainting;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(imagePainting + "Gevonden")) // bijhouden of het schilderij al is is opgehangen
                                                            // zodat als je de kamer kan verlaten en niet opnieuw moet beginnen

        {
            Painting.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (EquippedItemManager.Instance.EquippedItemName == imagePainting)
        {
            Painting.SetActive(true);
            Debug.Log("Painting" + imagePainting);
            GameObjectPainting = GameObject.FindWithTag("Painting" + imagePainting);
            Destroy(GameObjectPainting);
            EquippedItemManager.Instance.ClearEquippedItem();
            //Debug.Log(paintingName);
            PlayerPrefs.SetString(imagePainting + "Gevonden", "true");
            PlayerPrefs.Save();


        }
    }
}