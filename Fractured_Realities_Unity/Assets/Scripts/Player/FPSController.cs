using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 25f;
    public float runSpeed = 50f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public bool cursorLock = true;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public GameObject Inventory;
    public GameObject IngameMenuUI;
    public GameObject GuessingGame;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public GameObject guessingGameCanvas;
    public CharacterController characterController;
    Animator animator; // Reference to Animator

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); 
        
    }

    void Update()
    {

        if (Inventory.activeSelf == true || IngameMenuUI.activeSelf == true || GuessingGame.activeSelf == true || SceneManager.GetActiveScene().name == "Scoreboard")
        {
            cursorLock = false;
        }

        else
        {
            cursorLock = true;
        }

        if (cursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;

            #region Handles Movment
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Update Animator parameter "Move"
            float moveMagnitude = new Vector2(curSpeedX, curSpeedY).magnitude; // Calculate combined movement speed
                                                                               // animator.SetFloat("Move", moveMagnitude); // Update Move parameter
                                                                               //Debug.Log(moveMagnitude);
            #endregion

            #region Handles Jumping
            if (Input.GetKeyDown("space") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion

            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion
        }

        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            #region Handles Movment
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Update Animator parameter "Move"
            float moveMagnitude = new Vector2(curSpeedX, curSpeedY).magnitude; 
            #endregion

            #region Handles Jumping
            if (Input.GetKeyDown("space") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion

            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            //if (canMove)
            //{
            //    rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            //    rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //    transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            //}

            #endregion
        }



    }
}