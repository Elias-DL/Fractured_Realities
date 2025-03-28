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
    public string naamGezien;
    Ray ray;
    float sphereRadius = 2.0f; // Same radius as SphereCast
    float rayDistance = 100f;   // Max distance for the SphereCast
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
        // Perform SphereCast instead of Raycast
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit rayHit, rayDistance))
        {
            naamGezien = rayHit.transform.name;
            if (naamGezien == "Anklegrabber" || naamGezien == "ZombieWithBlood" || naamGezien == "Mutated")
            {
                Debug.Log(rayHit.transform.name);

            }

            gezien = true;

        }
        else
        {
            gezien = false;
        }

        yield return new WaitForSeconds(10f);
        gezien = false;
    }

    void OnDrawGizmos()
    {
        if (ray.origin != null)
        {
            // Draw a sphere at the ray's origin
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ray.origin, sphereRadius);

            // Draw spheres along the ray path to visualize the cast
            for (float i = 0; i < rayDistance; i += sphereRadius * 2)
            {
                Vector3 pointAlongRay = ray.origin + ray.direction * i;
                Gizmos.DrawWireSphere(pointAlongRay, sphereRadius);
            }
        }
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

        StartCoroutine(Scanning());

        animator.SetBool("WalkForward", false);
        animator.SetBool("WalkBackward", false);

        animator.SetBool("RunForward", false);
        animator.SetBool("RunBackward", false);


        animator.SetBool("Sneak", false);
        animator.SetBool("Jump", false);
        animator.SetBool("RightWalk", false);
        animator.SetBool("LeftWalk", false);

        //if (EquippedItemManager.Instance.EquippedItemName == "Camera")
        // {

        // }

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
