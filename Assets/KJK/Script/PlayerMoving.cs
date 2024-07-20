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
    private float zRotation = 0f;

    private float[] setAngles = { 0f, 90f, 180f, 270f, 360f };

    //Transform
    public Transform leftAlign;
    Quaternion targetRotation;
    float rotSpeed = 1.0f;

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
        /*Vector3 movementUpDown = Vector3.zero;
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

        rb.velocity = (movementUpDown + movementLeftRight) * speed;*/

    }

    public void Rotate()
    {

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = -1f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            xRotation = 0f;
            yRotation = 0f;
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Snap();
            //transform.rotation = Quaternion.identity;
            Debug.Log("The Snap key is pressed");
            return;

        }

        xRotation -= verticalInput * rotationSpeed * rotationSensitivityX * Time.deltaTime;
        yRotation += horizontalInput * rotationSpeed * rotationSensitivityY * Time.deltaTime;
        
        if (transform.rotation.z == 0)
        {
            targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    }

    public void Snap()
    {
        Vector3 currentRotation = transform.eulerAngles;
        xRotation = FindNearestAngle(currentRotation.x); 
        yRotation = FindNearestAngle(currentRotation.y);
        zRotation = FindNearestAngle(currentRotation.z);
        //Quaternion targetRotation = Quaternion.Euler(currentRotation.x, nearestAngle, currentRotation.z);
        //transform.rotation = targetRotation;
    }

    float FindNearestAngle(float currentAngle)
    {
        currentAngle = Mathf.Repeat(currentAngle, 360f);

        float nearest = setAngles[0];
        float minDifference = Mathf.Abs(currentAngle - nearest);

        foreach (float angle in setAngles)
        {
            float difference = Mathf.Abs(currentAngle - angle);
            if (difference < minDifference)
            {
                minDifference = difference;
                nearest = angle;
            }
        }

        return nearest;
    }
}

