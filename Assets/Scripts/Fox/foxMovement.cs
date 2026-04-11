using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Foxmove controls;

    Vector2 moveInput;
    CharacterController controller;

    public float speed = 5f;
    public float jumpHeight = 7f;
    public float gravity = -9.81f;

    float verticalVelocity;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Awake()
    {
        controls = new Foxmove();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        
        
        moveInput = controls.fox.move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);

        
        if (controls.fox.jump.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = jumpHeight; 
            
        }

        
        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }
}