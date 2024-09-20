using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;
    private float xRotation;
    private bool inSelectionMode = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // // Toggling cursor mode
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inSelectionMode)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            inSelectionMode = !inSelectionMode;
        }
        
        if (inSelectionMode) {return;}

        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        playerTransform.Rotate(Vector3.up, mouseX);

    }
}
