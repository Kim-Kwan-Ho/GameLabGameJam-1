using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 3f;

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

    private bool canRotate = false;
    private float[] setAngles = { 0f, 90f, 180f, 270f, 360f };

    //Transform
    Quaternion targetRotation;
    float rotSpeed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Debug.Log(rb);
    }

    void Update()
    {
        Move();
        Rotate();
    }
    public void Move()
    {
        Vector3 movementUpDown = Vector3.zero;
        Vector3 movementLeftRight = Vector3.zero;


        if (Input.GetKey(KeyCode.W))
        {
            movementUpDown = transform.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementUpDown = -transform.up;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementLeftRight = transform.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementLeftRight = -transform.right;
        }

        rb.velocity = (movementUpDown + movementLeftRight) * speed;
    }
    public void Rotate()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            Snap();
            //transform.rotation = Quaternion.identity;
            Debug.Log("The Snap key is pressed");
            return;

        }
              
    }

    public void Snap()
    {
        Vector3 currentRotation = transform.eulerAngles; // 현재의 로테이션 x,y,z값을 받아
        xRotation = FindNearestAngle(currentRotation.x); // 그리고 x,y,z에서 가장 가까운 앵글을 받아서 저장해
        yRotation = FindNearestAngle(currentRotation.y);
        zRotation = FindNearestAngle(currentRotation.z);
        Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        if (transform.rotation.z == 0)
        {
            targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        transform.rotation = targetRotation;
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

