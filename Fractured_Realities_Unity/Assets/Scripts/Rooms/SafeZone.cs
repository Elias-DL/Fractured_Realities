using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public GameObject Managers;
    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) // Zodat de monsters niet direct terug aanvallen als je dood bent gegaan, er is een kleien "safe place" is de map
    {
        if (other.CompareTag("Player"))
        {
            Managers.GetComponent<PlayerStats>().Respawning = false;
        }

    }
}
