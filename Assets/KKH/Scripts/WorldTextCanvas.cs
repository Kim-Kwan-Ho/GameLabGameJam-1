using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTextCanvas : MonoBehaviour
{
    private Transform _cameraTrs;

    private void Start()
    {
        _cameraTrs = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(_cameraTrs);
    }

}
