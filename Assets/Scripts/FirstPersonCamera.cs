using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of the mouse movement
    private float initialSensitivity;
    public Transform playerBody;          // Reference to the player's body for horizontal rotation
    private float xRotation = 0f;         // Tracks the up/down rotation (vertical)

    private bool canUseCamera = true;

    [SerializeField] private GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        initialSensitivity = mouseSensitivity;
        // Lock the cursor to the center of the screen and make it invisible
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(canUseCamera)
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player body horizontally (left/right)
            playerBody.Rotate(Vector3.up * mouseX);

            // Calculate the vertical rotation (clamped to avoid flipping the camera)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply the vertical rotation (up/down)
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

    }

    public void ToggleCamera(bool toggle) => canUseCamera = toggle;

    public void ChangeCameraSensitivity()
    {
        mouseSensitivity = Mathf.Lerp(initialSensitivity * 0.02f, initialSensitivity * 2, slider.GetComponent<Slider>().value);
        Debug.Log(mouseSensitivity);
    }
}
