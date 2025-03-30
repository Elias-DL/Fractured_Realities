using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingMonsters : MonoBehaviour
{

    public GameObject Image;
    public string imgMonster;
    public GameObject GameObjectMonster;
        // Start is called before the first frame update
    void Start()
    {
        
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

        }
    }
}
