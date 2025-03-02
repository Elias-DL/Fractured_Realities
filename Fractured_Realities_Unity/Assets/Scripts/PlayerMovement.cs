using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
        //declaratie
    public Rigidbody rb;
    public float forwardForce = 500, sideForce = 30, sprintForce = 1000, jump = 1000;
    public CharacterController characterController;
    Animator animator; // Reference to Animator
    public bool gezien;
    Ray ray;
    RaycastHit rayHit;
    public Camera cam;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Get the Animator from the child object
        
    }

    IEnumerator Scanning()
    {
        
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            // Debug.Log(rayHit.transform.name);
            gezien = true;
        }

        else
        {
            gezien = false;
        }

        yield return new WaitForSeconds(10f);
        gezien = false;
    }
    void Update()
    {
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(new Vector3(cam.transform.position.x, cam.transform.position.y - 10, cam.transform.position.z), transform.forward * 200, Color.red);
        //StartCoroutine(Scanning);   
            
        animator.SetBool("WalkForward", false);
        animator.SetBool("WalkBackward", false);

        animator.SetBool("RunForward", false);
        animator.SetBool("RunBackward", false);


        animator.SetBool("Sneak", false);
        animator.SetBool("Jump", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("LeftWalk", false);

        //animator.SetBool("RightRun", false);
        //animator.SetBool("LeftRun", false);

        if (Input.GetKey("w"))
        {
            rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
            animator.SetBool("WalkForward", true);

        }
        //if (Input.GetKey("w") && Input.GetKey("ctrl"))
        //{
        //    rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
        //}
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
            animator.SetBool("WalkBackward", true);

        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, sideForce * Time.deltaTime, 0);
            animator.SetBool("RightWalk", true);

        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -sideForce * Time.deltaTime, 0);
            animator.SetBool("LeftWalk", true);

        }

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w"))) // sprint gefixed?, voor zo de sliding te vermijden -> drag = 1? (nrml 0)
        {

            rb.AddForce(transform.forward * sprintForce * Time.deltaTime);
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", true);


         

        }

        if (Input.GetKey("space"))
        {

            rb.AddForce(0, jump, 0 * Time.deltaTime);
            animator.SetBool("Jump", true);

        }
    }

    private void StartCoroutine(Func<IEnumerator> scanning)
    {
        throw new NotImplementedException();
    }
}
