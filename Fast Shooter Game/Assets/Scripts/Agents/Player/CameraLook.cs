using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 200;
    public float aimSensModifier = 0.5f;
    float xRotation = 0f;

    float mouseX;
    float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float modifiedAimSens;
        float storedAimSens;
        storedAimSens = mouseSensitivity;
        if (Input.GetButton("Fire2"))
            modifiedAimSens = storedAimSens * aimSensModifier;
        else
            modifiedAimSens = storedAimSens;

        mouseX = Input.GetAxis("Mouse X") * modifiedAimSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * modifiedAimSens * Time.deltaTime;
    }
    public void HandleRotation(bool freeLook)
    {
        if (freeLook)
        {
            // add this later
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
