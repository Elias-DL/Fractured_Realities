using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //declaratie
    public Rigidbody rb;
    public AudioSource src;
    public AudioClip sfx1;
    public float forwardForce = 500, sideForce = 30, sprintForce = 1000, jump = 1000;
    public CharacterController characterController;
    Animator animator; // Reference to Animator
    public bool gezien;
    Ray ray;
    RaycastHit rayHit;
    public Camera cam;
    private string action;
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
        SoundEffects();
        action = null;

        if (src == null)
        {
            src = GetComponent<AudioSource>();

        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(new Vector3(transform.position.x, cam.transform.position.y - 3, transform.position.z), transform.forward * 200, Color.red);
        
        StartCoroutine(Scanning());

        animator.SetBool("WalkForward", false);
        animator.SetBool("WalkBackward", false);

        animator.SetBool("RunForward", false);
        animator.SetBool("RunBackward", false);


        animator.SetBool("Sneak", false);
        animator.SetBool("Jump", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("LeftWalk", false);

       

        if (Input.GetKey("w"))
        {
            rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
            animator.SetBool("WalkForward", true);
            action = "Walk";

        }
      
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
            animator.SetBool("WalkBackward", true);
            action = "Walk";
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, sideForce * Time.deltaTime, 0);
            animator.SetBool("RightWalk", true);
            action = "Walk";

        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -sideForce * Time.deltaTime, 0);
            animator.SetBool("LeftWalk", true);
            action = "Walk";

        }

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w")))
        {

            rb.AddForce(transform.forward * sprintForce * Time.deltaTime);
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", true);
            action = "Walk & Run";




        }

        if (Input.GetKey("space"))
        {

            rb.AddForce(0, jump, 0 * Time.deltaTime);
            animator.SetBool("Jump", true);
            action = "Jump";

        }
    }



    public void SoundEffects()
    {

        if (action == "Walk" && !src.isPlaying)
        {

            src.clip = sfx1;
            src.volume = 0.1f;
            src.Play();
        }

       


    }
}
