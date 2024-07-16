using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody rb;
    public enum viewState
    {
        TOP,
        SIDE
    }
    public static viewState view = viewState.SIDE;
    public float speed = 5.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            SideView();


    }

    void SideView()
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

    void TopView()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, 0, -speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

    }
}
