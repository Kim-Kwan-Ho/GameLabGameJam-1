using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall1_4Moving : MonoBehaviour
{
public enum WallType
    {
        PX,
        NX,
        PY,
        NY,
        PZ,
        NZ,
    }

    public WallType wallType;

    // Update is called once per frame

    public float speed = 10f;
    private bool stop = false;

    void Update()
    {
        float fixedspeed = speed * Time.deltaTime * 0.5f;
        if(!stop)
        {
            switch (wallType)
            {   
                case WallType.PX:
                    transform.position += new Vector3(fixedspeed, 0, 0);
                    break;
                case WallType.NX:
                    transform.position += new Vector3(-fixedspeed, 0, 0);
                    break;
                case WallType.PY:
                    transform.position += new Vector3(0, fixedspeed, 0);
                    break;
                case WallType.NY:
                    transform.position += new Vector3(0, -fixedspeed, 0);
                    break;
                case WallType.PZ:
                    transform.position += new Vector3(0, 0, fixedspeed);
                    break;
                case WallType.NZ:
                    transform.position += new Vector3(0, 0, -fixedspeed);
                    break;
            }


            if (transform.position.y > 40 || transform.position.y < -20 || transform.position.z > 30 || transform.position.z < -30 || transform.position.x > 30 || transform.position.x < -30)
            {
                Destroy(gameObject);
            }

        }
    }
}
