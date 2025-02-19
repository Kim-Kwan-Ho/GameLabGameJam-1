using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMode = startPlayerMode;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

}
