using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luik : MonoBehaviour
{

    public GameObject LuikDeur;
    public GameObject SPBarrels;
    public GameObject Valve;
    void Start()
    {
        LuikDeur = GameObject.FindWithTag("LuikDeur");
        SPBarrels = GameObject.FindWithTag("SPBarrels");
        Valve = GameObject.FindWithTag("Valve");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("close");
        LuikDeur.transform.position =  new Vector3(92, 51,178);
        SPBarrels.GetComponent<SpawnBarrels>().LuikBool = false;
        Valve.transform.rotation = Quaternion.Euler(90f,0f,0f);
    }
}
