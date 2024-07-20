using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestRotation : MonoBehaviour
{
    Vector3 rotationChange;
    public float sensitivity = 10;

    void Update()
    {
        rotationChange += new Vector3(Input.GetAxis("Vertical") * sensitivity, Input.GetAxis("Horizontal") * sensitivity, 0) * Time.deltaTime;
        transform.eulerAngles = rotationChange;
    }
}
