using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    Animator animator;
    bool isOpen = false; // Track the state of the drawer

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {

        if (isOpen == false)
        {
            animator.SetBool("Open", true);
            animator.SetBool("Close", false);
            isOpen = true;
        }

        else
        {
            animator.SetBool("Open", false);
            animator.SetBool("Close", true);
            isOpen = false;

        }

    }
}
