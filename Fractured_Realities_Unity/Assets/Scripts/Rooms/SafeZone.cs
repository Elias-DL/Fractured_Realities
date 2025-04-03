using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public GameObject Managers;
    // Start is called before the first frame update
    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Managers.GetComponent<PlayerStats>().Respawning = false;
        }

    }
}
