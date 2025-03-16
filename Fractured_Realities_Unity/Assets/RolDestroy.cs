using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolDestroy : MonoBehaviour
{

    public GameObject Managers;
    public GameObject TPNaar; // zelfde als sp als je de kamer binnenkomt

    // Start is called before the first frame update

    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");
        TPNaar = GameObject.FindWithTag("SP");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 18)
        {
            Destroy(gameObject);

        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Managers.GetComponent<PlayerStats>().Respawn(TPNaar);
        }
    }
}
