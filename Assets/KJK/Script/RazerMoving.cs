using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerMoving : MonoBehaviour
{
    public float speed = 10f;
    public enum RazerType
    {
        PX,
        NX,
        PY,
        NY,
        PZ,
        NZ,
    }
    public RazerType razerType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fixedspeed= speed*Time.deltaTime;
        switch (razerType)
        {
            case RazerType.PX:
                transform.position += new Vector3(fixedspeed, 0, 0);
                break;
            case RazerType.NX:
                transform.position += new Vector3(-fixedspeed, 0, 0);
                break;
            case RazerType.PY:
                transform.position += new Vector3(0, fixedspeed, 0);
                break;
            case RazerType.NY:
                transform.position += new Vector3(0, -fixedspeed, 0);
                break;
            case RazerType.PZ:
                transform.position += new Vector3(0, 0, fixedspeed);
                break;
            case RazerType.NZ:
                transform.position += new Vector3(0, 0, -fixedspeed);
                break;
        }

        if(transform.position.y > 40 || transform.position.y < -20 || transform.position.z > 30 || transform.position.z < -30 || transform.position.x > 30 || transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }
}
