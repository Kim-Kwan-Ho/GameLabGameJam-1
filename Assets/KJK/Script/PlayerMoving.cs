using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMoving : MonoBehaviour
{
    public enum PlayerMode
    {
        D3,
        D2
    }
    [SerializeField] private PlayerMode startPlayerMode = PlayerMode.D3;

    public static PlayerMode playerMode;
    Rigidbody rb;
    public float speed = 5.0f;

    //Rotation related variables
    public float rotationSpeed = 100f;
    public float smoothTime = 0.1f;
    public float rotationSensitivityX = 1f;
    public float rotationSensitivityY = 1f;
    public bool invertX = false;
    public bool invertY = false;

    private float xRotation = 0f;
    private float yRotation = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMode = startPlayerMode;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 movementUpDown = Vector3.zero;
        Vector3 movementLeftRight = Vector3.zero;


        if (Input.GetKey(KeyCode.UpArrow))
        {
            movementUpDown = transform.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movementUpDown = -transform.up;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementLeftRight = transform.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementLeftRight = -transform.right;
        }

        rb.velocity = (movementUpDown + movementLeftRight) * speed;

    }

    public void Rotate()
    {

        /*if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World );
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
        }*/

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = -1f; // Forward
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = 1f; // Backward
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = 1f; // Left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = -1f; // Right
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            xRotation = 0f;
            yRotation = 0f;
            transform.rotation = Quaternion.identity;
        }

        /*// Apply inversions to input values
        verticalInput *= invertX ? -1 : 1;
        horizontalInput *= invertY ? -1 : 1;*/

        // Calculate the target X rotation based on the vertical input
        xRotation -= verticalInput * rotationSpeed * rotationSensitivityX * Time.deltaTime;

        // Calculate the target Y rotation based on the horizontal input
        yRotation += horizontalInput * rotationSpeed * rotationSensitivityY * Time.deltaTime;

        // Create a quaternion for the rotation
        Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Smoothly interpolate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    }
}

