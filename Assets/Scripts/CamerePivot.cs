using UnityEngine;

public class CamerePivot : MonoBehaviour
{
    public Transform player;
    public float targetAngle = 45f;
    public float currentAngle = 0f;
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 4.4f;
    public float cameraAngle = 25f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0))
        {
            targetAngle += mouseX * mouseSensitivity;
        }
        else
        {
            targetAngle = Mathf.Round(targetAngle / 45);
            targetAngle *= 45;
        }
        
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }

        if (targetAngle > 360)
        {
            targetAngle -= 360;
        }

        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

        // Calculate the offset from the player based on the current angle
        transform.rotation = Quaternion.Euler(cameraAngle, currentAngle, -cameraAngle);

        // Set the camera position to the player's position plus the calculated offset
        transform.position = player.position;

        // Always look at the player
        transform.LookAt(player);
    }
}
