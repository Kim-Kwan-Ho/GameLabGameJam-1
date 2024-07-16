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
        if(view == viewState.SIDE)
        {
            SideView();
        }
        else if(view == viewState.TOP)
        {
            TopView();
        }
    }

    void SideView()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, speed, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, -speed, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        
        }

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
