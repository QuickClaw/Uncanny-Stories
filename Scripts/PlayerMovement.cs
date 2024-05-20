using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;
    private float sensitivity = 2f;
    private float gravity = -9.81f;

    private CharacterController characterController;
    private Camera mainCamera;
    private float rotationX;
    public bool isMoving;

    private Vector3 velocity;

    // Reference to the PlayerFootsteps script
    public PlayerFootsteps playerFootsteps;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        isMoving = moveDirection.magnitude > 0;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 moveVelocity = speed * moveDirection.normalized;
        characterController.Move((moveVelocity + velocity) * Time.deltaTime);

        playerFootsteps.PlayerMovement = this;

        if (!characterController.isGrounded)
            velocity.y += gravity * Time.deltaTime;
        else
            velocity.y = 0f;

        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
