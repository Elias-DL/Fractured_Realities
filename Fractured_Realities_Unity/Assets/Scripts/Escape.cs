using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escape : MonoBehaviour
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

    private void OnMouseDown()
    {
        Managers.GetComponent<PlayerStats>().Escape();

    }
}
