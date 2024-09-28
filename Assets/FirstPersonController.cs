using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;          // Speed of player movement
    [SerializeField] private float lookSpeed = 2f;          // Speed of mouse look
    [SerializeField] private float lookXLimit = 45f;        // Limit on the vertical look

    private float rotationX = 0;          // Current rotation on the X-axis

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the camera on the X-axis
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotate the player on the Y-axis
        transform.Rotate(0, mouseX, 0);

        // Move the player based on input
        float moveDirectionY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.TransformDirection(moveDirectionX, 0, moveDirectionY);
        transform.position += move;
    }
}
