using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float crouchingSpeed = 5f;
    
    public float gravity = -0.3f;
    public float jumpSpeed = 0.8f;
    private float baseLineGravity;

    private float currentSpeed;

    private float moveX;
    private float moveZ;
    private Vector3 move;

    public float timesDoubleJumped = 0f;

    private Rigidbody rb;

    public CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkingSpeed;
        rb = GetComponent<Rigidbody>();
        baseLineGravity = gravity;

    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        move = transform.right * moveX +
            transform.up * gravity +
            transform.forward * moveZ;
        characterController.Move(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runningSpeed;
        }

        else
        {
            currentSpeed = walkingSpeed;
        }

        if (characterController.isGrounded)
        {
            timesDoubleJumped = 0;
        }



        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            gravity = baseLineGravity;
            gravity *= -jumpSpeed;

        }

        else if (timesDoubleJumped <= 2 && Input.GetButtonDown("Jump"))
        {
            gravity = baseLineGravity;
            gravity *= -jumpSpeed;
            timesDoubleJumped++;
        }

        //Input.GetButton("Jump")



            if (gravity > baseLineGravity)
            {
                gravity -= 2 * Time.deltaTime;
            }
        
    }
}
