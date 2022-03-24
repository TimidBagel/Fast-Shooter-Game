using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerWalkSpeed = 2f;
    public float playerRunSpeed = 4f;
    public float playerSpeed;
    public float playerJumpHeight = 0.75f;
    public float gravity = -9.81f;

    public Vector3 velocity;

    public CameraLook playerCamera;
    public CharacterController controller;

    public bool freeLook = false;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;

    //public GameObject weapon1;
    //public GameObject weapon2;
    //public GameObject weapon3;

    private void Start()
    {
        playerCamera = Camera.main.GetComponent<CameraLook>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        #region for testing
  //      if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
  //          weapon1.SetActive(true);
  //          weapon2.SetActive(false);
  //          weapon3.SetActive(false);
		//}
  //      if (Input.GetKeyDown(KeyCode.Alpha2))
		//{
  //          weapon1.SetActive(false);
  //          weapon2.SetActive(true);
  //          weapon3.SetActive(false);
		//}
  //      if (Input.GetKeyDown(KeyCode.Alpha3))
		//{
  //          weapon1.SetActive(false);
  //          weapon2.SetActive(false);
  //          weapon3.SetActive(true);
		//}
  //      if (Input.GetKeyDown(KeyCode.H) && weapon1.activeSelf)
  //          weapon1.SetActive(false);
  //      if (Input.GetKeyDown(KeyCode.H) && weapon2.activeSelf)
  //          weapon2.SetActive(false);
  //      if (Input.GetKeyDown(KeyCode.H) && weapon3.activeSelf)
  //          weapon3.SetActive(false);
        #endregion

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetMouseButton(2))
        {
            freeLook = true;
        }
        else
        {
            freeLook = false;
        }

        playerCamera.HandleRotation(freeLook);

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = playerRunSpeed;
            if ((xMovement > 0 || xMovement < 0) && (zMovement > 0 || zMovement < 0))
                playerSpeed = playerRunSpeed / 2;
        }
        else
        {
            playerSpeed = playerWalkSpeed;
            if ((xMovement > 0 || xMovement < 0) && (zMovement > 0 || zMovement < 0))
                playerSpeed = playerWalkSpeed / 2;
        }

        Vector3 movement = transform.right * xMovement + transform.forward * zMovement;

        controller.Move(movement * playerSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerJumpHeight * -2f * gravity);
        }
    }
}

