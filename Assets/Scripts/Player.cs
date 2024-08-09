using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody player;
    public Camera playerCamera;

    [Header("Movement")]
    float vertical;
    float horizontal;

    float verticalRaw;
    float horizontalRaw;

    Vector3 targetRotation;

    public float rotationSpeed = 10;
    public float speed = 200f;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        horizontalRaw = Input.GetAxisRaw("Horizontal");
        verticalRaw = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(horizontal, 0, vertical);
        Vector3 inputRaw = new Vector3(horizontalRaw, 0, verticalRaw);

        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        if (inputRaw.sqrMagnitude > 1f)
        {
            inputRaw.Normalize();
        }

        if (inputRaw != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input).eulerAngles;
        }

        player.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x, Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);

        Vector3 vel = input * speed * Time.deltaTime;
        player.linearVelocity = new Vector3(vel.x, player.linearVelocity.y, vel.z);
    }
}
