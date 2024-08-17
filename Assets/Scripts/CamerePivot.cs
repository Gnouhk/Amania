using UnityEngine;

public class CamerePivot : MonoBehaviour
{
    public Transform player;
    public float targetAngle = 45f;
    public float currentAngle = 0f;
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 4.4f;
    public float cameraAngle = 25f;
    public float distanceFromPlayer = 5f;

    private float[] angleHistory = new float[2];  // Array to store previous and current target angles
    private bool isColliding = false;

    private void Start()
    {
        angleHistory[0] = targetAngle;  // Previous target angle
        angleHistory[1] = targetAngle;  // Current target angle
    }

    private void Update()
    {
        if (isColliding)
        {
            // Revert to the previous target angle
            targetAngle = angleHistory[0];
            currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(cameraAngle, currentAngle, -cameraAngle);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");

            if (Input.GetMouseButton(0))
            {
                targetAngle += mouseX * mouseSensitivity;
            }
            else
            {
                targetAngle = Mathf.Round(targetAngle / 45) * 45;
            }

            if (targetAngle < 0)
            {
                targetAngle += 360;
            }

            if (targetAngle > 360)
            {
                targetAngle -= 360;
            }

            // Update the angle history before applying the new angle
            angleHistory[0] = angleHistory[1];  // Shift current target to previous
            angleHistory[1] = targetAngle;  // Set current target angle

            currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(cameraAngle, currentAngle, -cameraAngle);
        }

        // Set the camera position to the player's position plus the calculated offset
        transform.position = player.position;

        // Always look at the player
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Camera is colliding with terrain, reverting to last safe angle.");
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Camera is no longer colliding with terrain, resuming normal rotation.");
            isColliding = false;
        }
    }
}