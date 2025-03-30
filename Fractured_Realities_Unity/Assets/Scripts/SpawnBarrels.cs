using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrels : MonoBehaviour
{

    public GameObject Barrel;
    private bool Spawning;
    public bool LuikBool;// public zodat je het kan aansperekn bij dicht doen van het luik dat ze niet verder spawnen (spawning op false zetten)
    // Start is called before the first frame update
    void Start()
    {
        LuikBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawning == false && LuikBool == true)
        {
            StartCoroutine(Barrels()); 
            Spawning = true;
        }
    }


    IEnumerator Barrels()
    {

        Instantiate(Barrel, transform.position, Quaternion.Euler(0, 0, 90));

        yield return new WaitForSeconds(5);

        Spawning = false;
    }
}
