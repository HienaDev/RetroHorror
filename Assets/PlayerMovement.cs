using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private KeyCode front = KeyCode.W;
    [SerializeField] private KeyCode back = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gravity = 9.8f;

    private CharacterController characterController;
    private Vector3 velocity;
    private float verticalSpeed = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Reset horizontal velocity each frame
        velocity = Vector3.zero;

        // Horizontal movement input
        if (Input.GetKey(front))
        {
            velocity.z += 1;
        }
        if (Input.GetKey(back))
        {
            velocity.z -= 1;
        }
        if (Input.GetKey(left))
        {
            velocity.x -= 1;
        }
        if (Input.GetKey(right))
        {
            velocity.x += 1;
        }

        // Normalize and apply movement speed to horizontal velocity
        velocity = velocity.normalized * movementSpeed;

        // Convert local movement direction to world space based on the player's rotation
        Vector3 worldVelocity = transform.TransformDirection(velocity);

        // Apply gravity manually
        if (characterController.isGrounded)
        {
            verticalSpeed = 0f;  // Reset vertical speed when grounded
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;  // Apply gravity
        }

        // Combine horizontal movement and vertical speed
        worldVelocity.y = verticalSpeed;

        // Move the character controller
        characterController.Move(worldVelocity * Time.deltaTime);
    }

    public bool GetHasSpeed()
    {
        return (velocity != Vector3.zero && characterController.isGrounded);
    }
}
