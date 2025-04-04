using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class escape : MonoBehaviour
{
    public GameObject Managers;
    [SerializeField] private string neccescaryKey;

    // Start is called before the first frame update
    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        string equippedItem = EquippedItemManager.Instance.EquippedItemName;

        if (equippedItem == neccescaryKey)
        {
            Managers.GetComponent<PlayerStats>().Escape();

        }

        
    }
}
