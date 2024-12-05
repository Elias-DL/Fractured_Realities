using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
        //declaratie
    public Rigidbody rb;
    public float forwardForce = 500, sideForce = 30, sprintForce = 1000, jump = 10;



    void Update()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, sideForce * Time.deltaTime, 0);
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -sideForce * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w"))) // sprint gefixed?, voor zo de sliding te vermijden -> drag = 1? (nrml 0)
        {
            rb.AddForce(transform.forward * sprintForce * Time.deltaTime);
        }

        if (Input.GetKeyDown("space"))
        {

            rb.AddForce(0, jump, 0 * Time.deltaTime);
        }
    }
}
