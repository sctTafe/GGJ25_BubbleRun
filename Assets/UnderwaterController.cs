using UnityEngine;
using UnityEngine.InputSystem;

public class UnderwaterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardForce = 10f;
    public float strafeForce = 8f;
    public float drag = 2f;

    public Transform targetCamera; // Assign the camera in the Inspector

    private Rigidbody rb;
    private SwimmingPlayerInput playerInput;
    private Vector2 moveInput;
    private Vector2 mouseDelta;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new SwimmingPlayerInput();
        playerInput.CharacterControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.CharacterControls.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.blue);
    }
    private void FixedUpdate()
    {
        // Rotate the character based on mouse input
        RotatePlayer();

        // Apply forces for movement
        MovePlayer();
    }

    private void RotatePlayer()
    {
        // Cast a ray from the camera forward
        Ray ray = new Ray(targetCamera.transform.position, targetCamera.transform.forward);
        Vector3 targetPosition = ray.GetPoint(100f); // Point 100 units in front of the camera

        // Draw a debug ray in blue
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);

        // Rotate the object to face the target position
        Vector3 direction = targetPosition - transform.position;
        //direction.y = 0; // Keep the rotation on the horizontal plane
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void MovePlayer()
    {
        // Forward and backward (W and S)
        Vector3 forwardForceVector = transform.forward * moveInput.y * forwardForce;

        // Strafing (A and D)
        Vector3 strafeForceVector = transform.right * moveInput.x * strafeForce;

        // Apply the forces
        rb.AddForce(forwardForceVector + strafeForceVector, ForceMode.Acceleration);

        // Apply drag to slow down the swimmer when not moving
        rb.linearVelocity *= (1 - drag * Time.fixedDeltaTime);
    }
}
