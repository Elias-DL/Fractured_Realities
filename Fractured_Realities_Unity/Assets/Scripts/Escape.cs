using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escape : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Player.GetComponent<PlayerStats>().Escape();

    }
}
