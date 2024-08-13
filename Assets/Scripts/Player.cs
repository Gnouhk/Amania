using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody player;
    public Camera playerCamera;

    [Header("Movement")]
    public float speed = 200f;
    public float rotationSpeed = 10f;
    public float uprightSpeed = 5f; // Speed for returning to upright position
    public float jumpForce = 5f;    // The force applied when jumping

    float vertical;
    float horizontal;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Use the camera's direction for movement
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * vertical + right * horizontal;

        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        // Move player
        Vector3 velocity = direction * speed * Time.deltaTime;
        velocity.y = player.linearVelocity.y; // Maintain existing y-velocity (for gravity and jumps)


        player.linearVelocity = velocity;

        // Rotate player to face movement direction
        if (direction != Vector3.zero)
        {
            Vector3 targetRotation = Quaternion.LookRotation(direction).eulerAngles;
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, targetRotation.y, 0), Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Smoothly transition back to upright when not moving
            Quaternion uprightRotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0);
            player.rotation = Quaternion.Slerp(player.rotation, uprightRotation, Time.deltaTime * uprightSpeed);
        }
    }
}