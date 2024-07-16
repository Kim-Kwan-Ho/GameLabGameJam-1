using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            Move();


    }

    void Move()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement = transform.up;
            Debug.Log("W");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement = -transform.right;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = -transform.up;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = transform.right;
        }

        rb.velocity = movement * speed;

    }
    
}
