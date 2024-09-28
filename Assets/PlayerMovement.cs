using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private KeyCode front = KeyCode.W;
    [SerializeField] private KeyCode back = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody rb;
    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.zero;

        if(Input.GetKey(front))
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


        // Normalize the velocity to ensure consistent movement speed in all directions
        velocity = velocity.normalized * movementSpeed;

        // Convert the velocity from local space to world space based on the player's current rotation
        Vector3 worldVelocity = transform.TransformDirection(velocity);

        // Apply the transformed velocity to the Rigidbody
        rb.linearVelocity = worldVelocity;

    }
}
