using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 200f;

    public Transform playerBody;

    public float xRotation = 0f;
    public float yRotation = 0f;

    public Transform cameraRotator;

    public bool AllowCharacterRotation = true;

    public bool SkipPlayerCamera = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (SkipPlayerCamera) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        if (!SkipPlayerCamera) xRotation = Mathf.Clamp(xRotation, -80f, 70f);
        //xRotation = Mathf.Clamp(xRotation, -80f, 70f);

        if (AllowCharacterRotation)
        {
            playerBody.Rotate(Vector3.up * mouseX);
            yRotation = 0f;
        }
        else
        {
            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -30f, 30f);
        }

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        if (!AllowCharacterRotation)
        {
            cameraRotator.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }


    }

    public void DisableCharacterRotation()
    {

        AllowCharacterRotation = false;
    }

    public void EnableCharacterRotation()
    {
        cameraRotator.localRotation = Quaternion.identity;
        yRotation = 0f;
        AllowCharacterRotation = true;

    }
}
