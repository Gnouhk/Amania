using UnityEngine;

public class CamerePivot : MonoBehaviour
{
    public Transform player;
    public Transform cameraChild;
    public float targetAngle = 0f;
    public float currentAngle = 0f;
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 4.4f;
    public float cameraAngle;

    private float collisionReversalDegree = 5f;  // Degree to reverse when collidin
    private bool isColliding = false;
    private bool isRotatingLeft = false;  // Track the current rotation direction

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        if (isColliding)
        {
            // Reverse the target angle based on the rotation direction
            float reversalDegree = isRotatingLeft ? collisionReversalDegree : -collisionReversalDegree;
            targetAngle = NormalizeAngle(targetAngle + reversalDegree);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                // Determine rotation direction based on mouse input
                isRotatingLeft = mouseX < 0;

                // Update the target angle based on mouse input
                targetAngle += mouseX * mouseSensitivity;
            }

            targetAngle = NormalizeAngle(targetAngle);
        }

        // Smoothly rotate towards the target angle
        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

        // Apply the rotation and position updates
        transform.rotation = Quaternion.Euler(cameraAngle, currentAngle, 0f);
        transform.position = player.position;

        // Always look at the player
        transform.LookAt(player);
    }

    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle > 360) angle -= 360;
        return angle;
    }

    public void OnChildTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Camera child collider is colliding with terrain, reversing angle.");
            isColliding = true;
        }
    }

    public void OnChildTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Camera child collider is no longer colliding with terrain, resuming normal rotation.");
            isColliding = false;
        }
    }

}