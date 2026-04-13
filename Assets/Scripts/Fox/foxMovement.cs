using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    foxmove controls;

    Vector2 moveInput;
    CharacterController controller;

    public float speed = 5f;
    public float jumpHeight = 7f;
    public float gravity = -9.81f;
    public float lookInput;

    float verticalVelocity;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Awake()
    {
        InitializeControls();

        // Debug.Log(controls);
        // Debug.Log(controls.fox);
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


        //if contrrol.fox make nullreference exception catch and initialize controls again
        try
        {
            moveInput = controls.fox.move.ReadValue<Vector2>();
            lookInput = controls.fox.look.ReadValue<float>();
        }
        catch (NullReferenceException)
        {
            InitializeControls();
        }



        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);




        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        // if (move.sqrMagnitude > 0.001f)
        // {
        //     Quaternion targetRot = Quaternion.LookRotation(-move) * Quaternion.Euler(0, 90f, 0); transform.rotation = Quaternion.Slerp(
        //                                transform.rotation,
        //                                targetRot,
        //                                10f * Time.deltaTime
        //                            );
        // }


        if (Mathf.Abs(lookInput) > 0.01f)
        {
            transform.Rotate(Vector3.up * lookInput * 200f * Time.deltaTime);
        }

        controller.Move(move * speed * Time.deltaTime);


        if (controls.fox.jump.WasPressedThisFrame() && isGrounded && transform.position.y < 2f)
        {
            verticalVelocity = jumpHeight;

        }


        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }


    public void InitializeControls()
    {
        controls = new foxmove();
        controls.Enable();
    }
}