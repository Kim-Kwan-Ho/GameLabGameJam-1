using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputAction pressed, axis;
    private Transform cam;
    private bool rotateAllowed;
    private Vector2 rotation;
    [SerializeField] private float speed = 1f;
    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while (rotateAllowed)
        {
            //apply rotation
            rotation *= speed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(-cam.right, rotation.y, Space.World);
            yield return null;
        }
    }
}
