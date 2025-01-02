using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform player;

    // dit lijkt me veeeeeeel te gemakkelijk, je hebt ook met AI dinge zoals meeste tutorials maar idk snap daar niks van , oke ja dit gaat niet met muren enzo 
    void Update()
    {

        if (player.position.x - transform.position.x < 100 || player.position.x + transform.position.x > 100)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }

        else
        {

        }
    }
}
