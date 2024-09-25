using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;

    private float xRotation;
    private bool inSelectionMode = false;
    
    [SerializeField] private float bobResetSpeed;
    [SerializeField] private float bobbingSpeed;
    [SerializeField] private float verticalBobDistance;
    [SerializeField] private float horizontalBobDistance;

    private bool isBobbing = false;
    private Vector3 startingCamPos;
    private float verticalSinAngle = 180f;
    private float horizontalSinAngle = 0f;

    [SerializeField] private Transform footstepSoundOrigin;
    private bool footSoundPlayed = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startingCamPos = transform.localPosition;
    }

    void Update()
    {
        HandleSelectionMode();
        HandleBobbing();
        if (inSelectionMode) {return;}

        HandleMouseMovement();
    }

    void HandleMouseMovement()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        playerTransform.Rotate(Vector3.up, mouseX);
    }

    void HandleSelectionMode()
    {
        // Toggling cursor mode
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
    }

    private void HandleBobbing()
    {
        if (isBobbing)
        {

            // SoundManager.PlaySound(SoundLibrary.Singleton.metalFootstep, transform.position, 0.075f, 0.2f);
            if (verticalSinAngle >= 360) { verticalSinAngle = 180; footSoundPlayed = false; }
            if (horizontalSinAngle >= 360) horizontalSinAngle = 0;

            if (verticalSinAngle >= 270f && !footSoundPlayed) 
            {
                SoundManager.PlaySound(SoundLibrary.Singleton.metalFootstep, footstepSoundOrigin.position, 0.11f, 0.2f);
                footSoundPlayed = true;
            }

            verticalSinAngle += Time.deltaTime * bobbingSpeed;
            horizontalSinAngle += Time.deltaTime * bobbingSpeed;
            
            float verticalOffset = Mathf.Sin(Mathf.Deg2Rad * verticalSinAngle);
            float horizontalOffset = Mathf.Sin(Mathf.Deg2Rad * horizontalSinAngle);
            Vector3 totalOffset = new Vector3(horizontalOffset * horizontalBobDistance, verticalOffset * verticalBobDistance, 0f);
            
            transform.localPosition = startingCamPos + totalOffset;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startingCamPos, bobResetSpeed * Time.deltaTime);
        }
    }
    

    public void StartBobbing()
    {
        if (!isBobbing)
        {
            isBobbing = true;
            verticalSinAngle = 180f;
            horizontalSinAngle = 0f;
        }
    }

    public void StopBobbing() { isBobbing = false; footSoundPlayed = false; }
}
