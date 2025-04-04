using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //declaratie
    public Rigidbody rb;
    public AudioSource src;
    public AudioClip sfxWalk;
    public AudioClip sfxRun;

    public AudioClip sfxJump;

    public float forwardForce = 500, sideForce = 30, sprintForce = 1000, jump = 1000;
    public CharacterController characterController;
    Animator animator; // Reference to Animator
    public bool gezien;
    public string naamGezien;
    Ray ray;
    float sphereRadius = 1.0f; // Same radius as SphereCast
    float rayDistance = 100f;   // Max distance for the SphereCast
    RaycastHit rayHit;
    public Camera cam;
    private string action;
    public TextMeshProUGUI txtTips;
    // Add a LayerMask variable to select specific layers
    public LayerMask layerMaskEnemies;

    private void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Get the Animator from the child object
    }

    IEnumerator Scanning()
    {
        // Perform SphereCast using a LayerMask to only detect specific layers
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit rayHit, rayDistance, layerMaskEnemies))
        {
            naamGezien = rayHit.transform.name;
            if (naamGezien == "AnkleGrabber" || naamGezien == "Bookhead" || naamGezien == "Zombie")
            {
                Debug.Log(rayHit.transform.name);
                gezien = true;
            }
        }
        else
        {
            gezien = false;
            naamGezien = null;
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
        if (EquippedItemManager.Instance.EquippedItemName == null)
        {

            txtTips.text = "";
        }
        else if (EquippedItemManager.Instance.EquippedItemName == "Flashlight")
        {
            txtTips.text = "Press F to turn on/off flashlight \nPress R to unequip";

        }
        else if (EquippedItemManager.Instance.EquippedItemName.Contains("Key"))
        {
            txtTips.text = "Find the right door to unlock it \nPress R to unequip";
        }
        else if (EquippedItemManager.Instance.EquippedItemName == "Camera")
        {
            txtTips.text = "Press F when nearby a monster to take a picture \nPress R to unequip";
        }

        else if (EquippedItemManager.Instance.EquippedItemName == "Candle")
        {
            txtTips.text = "Use the light to find the hidden key \nPress R to unequip";
        }

        else if (EquippedItemManager.Instance.EquippedItemName == "USB") // check usb naam
        {
            txtTips.text = "";
        }

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

        if (Input.GetKey("w"))
        {
            //rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
            animator.SetBool("WalkForward", true);
            action = "Walk";
        }

        if (Input.GetKey("s"))
        {
            //rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
            animator.SetBool("WalkBackward", true);
            action = "Walk";
        }

        if (Input.GetKey("d"))
        {
            //transform.Rotate(0, sideForce * Time.deltaTime, 0);
            animator.SetBool("RightWalk", true);
            action = "Walk";
        }

        if (Input.GetKey("a"))
        {
            //transform.Rotate(0, -sideForce * Time.deltaTime, 0);
            animator.SetBool("LeftWalk", true);
            action = "Walk";
        }

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w")))
        {
            //rb.AddForce(transform.forward * sprintForce * Time.deltaTime);
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", true);
            action = "Walk & Run";
        }

        if (Input.GetKey("space"))
        {
            //rb.AddForce(0, jump, 0 * Time.deltaTime);
            animator.SetBool("Jump", true);
            action = "Jump";
        }

        SoundEffects();

    }

    public void SoundEffects()
    {
        if (action == "Walk" && !src.isPlaying)
        {
            src.clip = sfxWalk;
            src.volume = 0.1f;
            src.Play();

        }

        else if (action == "Walk & Run" && !src.isPlaying)
        {
            src.clip = sfxRun;
            src.volume = 0.1f;
            src.Play();
        }

        else if (action == "Jump" && !src.isPlaying)
        {
            src.clip = sfxJump;
            src.volume = 0.1f;
            src.Play();
        }
    }
}
