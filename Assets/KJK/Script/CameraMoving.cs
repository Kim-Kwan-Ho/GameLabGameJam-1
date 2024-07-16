using UnityEngine;
using System.Collections;
using System;

public class CameraMoving : MonoBehaviour
{
    public Transform target;  // 카메라가 바라볼 중심점
    public float distance = 10.0f;  // 중심점에서의 거리
    public float orbitSpeed = 10.0f;  // 카메라가 회전하는 속도
    private bool isOrbiting = false;  // 코루틴 실행 중 여부 확인용 변수
    public float cooltime = 1.0f;  // 쿨타임
    public GameObject player;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
            enabled = false;
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");

        // 초기 각도 설정
        this.transform.position = new Vector3(0, target.position.y, target.position.z - distance);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isOrbiting)
        {
            StartCoroutine(OrbitCooldown());
            StartCoroutine(OrbitAround(0));
        }
        if (Input.GetKeyDown(KeyCode.A) && !isOrbiting)
        {
            StartCoroutine(OrbitCooldown());
            StartCoroutine(OrbitAround(1));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isOrbiting)
        {
            StartCoroutine(OrbitCooldown());
            StartCoroutine(OrbitAround(2));
        }
        if  (Input.GetKeyDown(KeyCode.S) && !isOrbiting)
        {
            StartCoroutine(OrbitCooldown());
            StartCoroutine(OrbitAround(3));
        }
        //transform.LookAt(target);
    }

    IEnumerator OrbitAround(int caseNum)
    {
        float curTimescale = Time.timeScale;
        Time.timeScale = 0;
        float starttime = Time.realtimeSinceStartup;
        if(caseNum == 0)
        {
            float rotProgress = 0;
            while (true)
            {
                float step = orbitSpeed*(Time.realtimeSinceStartup-starttime);

                if (rotProgress + step > 90)
                {
                    step = 90 - rotProgress;
                }
                transform.RotateAround(target.position, transform.right, step);
                rotProgress += step;
                if(rotProgress >= 90)
                {
                    break;
                }
                yield return null;
                
            }
            player.transform.Rotate(90, 0, 0);
        }

        if(caseNum == 1)
        {
            float rotProgress = 0;
            while (true)
            {
                float step = orbitSpeed*(Time.realtimeSinceStartup-starttime);

                if (rotProgress + step > 90)
                {
                    step = 90 - rotProgress;
                }
                transform.RotateAround(target.position, transform.up, step);
                rotProgress += step;
                if(rotProgress >= 90)
                {
                    break;
                }
                yield return null;
                
            }
            player.transform.Rotate(0, 90, 0);
        }

 
        if(caseNum == 2)
        {
            float rotProgress = 0;
            while (true)
            {
                float step = orbitSpeed*(Time.realtimeSinceStartup-starttime);

                if (rotProgress + step > 90)
                {
                    step = 90 - rotProgress;
                }
                transform.RotateAround(target.position, -transform.up, step);
                rotProgress += step;
                if(rotProgress >= 90)
                {
                    break;
                }
                yield return null;
                
            }
            player.transform.Rotate(0, -90, 0);
        }       

        if(caseNum == 3)
        {
            float rotProgress = 0;
            while (true)
            {
                float step = orbitSpeed*(Time.realtimeSinceStartup-starttime);

                if (rotProgress + step > 90)
                {
                    step = 90 - rotProgress;
                }
                transform.RotateAround(target.position, -transform.right, step);
                rotProgress += step;
                if(rotProgress >= 90)
                {
                    break;
                }
                yield return null;
                
            }
            player.transform.Rotate(-90, 0, 0);           
        }
        
        Time.timeScale = curTimescale;
    }
    IEnumerator OrbitCooldown()
    {
        isOrbiting = true;
        yield return new WaitForSeconds(0.1f);
        isOrbiting = false;
    }
}
