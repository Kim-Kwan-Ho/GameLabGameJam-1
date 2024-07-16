using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallMoving : MonoBehaviour
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
    private bool back = false;
    private bool coru = true;
    void Update()
    {
        float fixedspeed = speed * Time.deltaTime * 0.5f;
        if(!stop)
        {
            switch (wallType)
            {   
                case WallType.PX:
                    transform.position += new Vector3(fixedspeed, 0, 0);
                    if(transform.position.x > -5)
                    {
                        transform.position = new Vector3(-5, transform.position.y, transform.position.z);
                        stop = true;
                    }
                    break;
                case WallType.NX:
                    transform.position += new Vector3(-fixedspeed, 0, 0);
                    if(transform.position.x < 5)
                    {
                        transform.position = new Vector3(5, transform.position.y, transform.position.z);
                        stop = true;
                    }
                    break;
                case WallType.PY:
                    transform.position += new Vector3(0, fixedspeed, 0);
                    if(transform.position.y > 5)
                    {
                        transform.position = new Vector3(transform.position.x, 5, transform.position.z);
                        stop = true;
                    }
                    break;
                case WallType.NY:
                    transform.position += new Vector3(0, -fixedspeed, 0);
                    if(transform.position.y < 15)
                    {
                        transform.position = new Vector3(transform.position.x, 15, transform.position.z);
                        stop = true;
                    }
                    break;
                case WallType.PZ:
                    transform.position += new Vector3(0, 0, fixedspeed);
                    if(transform.position.z > -5)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
                        stop = true;
                    }
                    break;
                case WallType.NZ:
                    transform.position += new Vector3(0, 0, -fixedspeed);
                    if(transform.position.z < 5)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 5);
                        stop = true;
                    }
                    break;
            }


        }
            if(stop && coru)
            {
                StartCoroutine(WallCooldown());
                coru = false;
            }

            if(back)
            {
                switch(wallType)
                {
                    case WallType.PX:
                        transform.position += new Vector3(-fixedspeed, 0, 0);
                        break;
                    case WallType.NX:
                        transform.position += new Vector3(fixedspeed, 0, 0);
                        break;
                    case WallType.PY:
                        transform.position += new Vector3(0, -fixedspeed, 0);
                        break;
                    case WallType.NY:
                        transform.position += new Vector3(0, fixedspeed, 0);
                        break;
                    case WallType.PZ:
                        transform.position += new Vector3(0, 0, -fixedspeed);
                        break;
                    case WallType.NZ:
                        transform.position += new Vector3(0, 0, fixedspeed);
                        break;
                }
            }
        if (transform.position.y > 40 || transform.position.y < -20 || transform.position.z > 30 || transform.position.z < -30 || transform.position.x > 30 || transform.position.x < -30)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator WallCooldown()
    {
        float fixedspeed = speed * Time.deltaTime;
        yield return new WaitForSeconds(7f);
        back = true;
        
    }

}
