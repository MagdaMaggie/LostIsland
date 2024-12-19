using UnityEngine;
using UnityEditor;

public class MyFPScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Walking speed (meters/second)")]
    public float walkSpeed = 5.0f;

    [Tooltip("Backward speed (meters/second)")]
    public float backwardSpeed = 2.5f; 

    [Tooltip("Running speed multiplier")]
    public float runMultiplier = 2.0f;

    [Tooltip("Jump force")]
    public float jumpForce = 5.0f;

    [Tooltip("Degrees per pixel for rotation sensitivity")]
    public float rotScale = 1.0f;

    [Tooltip("Transform of the camera or head object")]
    public Transform head;

    [Header("Physics Settings")]
    [Tooltip("Gravity force")]
    public float gravity = -9.81f;

    [Tooltip("Ground check distance")]
    public float groundCheckDistance = 0.4f;

    [Tooltip("Layer for ground detection")]
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float verticalRotation = 0f;
    private Vector3 mousePosPrevious;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogWarning("CharacterController component is missing on the GameObject. Please add one in the Inspector.");
            enabled = false; 
            return;
        }

        groundMask = LayerMask.GetMask("Terrain");
        if (groundMask == 0)
        {
            Debug.LogError("No layer named 'Terrain' found. Please ensure the 'Terrain' layer exists in your project.");
        }
        else
        {
            Debug.Log($"Ground mask set to Terrain layer with mask value: {groundMask.value}");
        }

        //UnityEditor.EditorUtility.SetDirty(this);

        mousePosPrevious = Input.mousePosition;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        ApplyGravity();
    }

    void HandleMouseLook()
    {
        Vector3 mousePosCurrent = Input.mousePosition;
        Vector3 mouseDelta = mousePosCurrent - mousePosPrevious;
        mousePosPrevious = mousePosCurrent;

        float deltaRotY = mouseDelta.x * rotScale;
        transform.Rotate(0.0f, deltaRotY, 0.0f);

       
        float deltaRotX = mouseDelta.y * rotScale;
        verticalRotation -= deltaRotX;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        head.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
    }

    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; 


        float speed = walkSpeed;
        Vector3 moveDirection = Vector3.zero;


        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = backwardSpeed; 
            moveDirection -= transform.forward;
        }


        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        if (Input.GetKey(KeyCode.LeftShift) && moveDirection != Vector3.zero)
        {
            speed *= runMultiplier; 
        }


        moveDirection.Normalize();
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
