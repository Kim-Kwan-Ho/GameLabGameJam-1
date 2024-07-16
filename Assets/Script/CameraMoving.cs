using UnityEngine;
using System.Collections;
using System;

public class CameraMoving : MonoBehaviour
{
    public Transform target;  // 카메라가 바라볼 중심점
    public float distance = 10.0f;  // 중심점에서의 거리
    public float orbitSpeed = 10.0f;  // 카메라가 회전하는 속도

    private float angle = 0.0f;  // 현재의 각도
    private float Yangle = 0.0f;
    private bool isOrbiting = false;  // 코루틴 실행 중 여부 확인용 변수
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
            StartCoroutine(OrbitAround(0));
        }
        if (Input.GetKeyDown(KeyCode.A) && !isOrbiting)
        {
            StartCoroutine(OrbitAround(1));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isOrbiting)
        {
            StartCoroutine(OrbitAround(2));
        }
        if  (Input.GetKeyDown(KeyCode.S) && !isOrbiting)
        {
            StartCoroutine(OrbitAround(3));
        }


        //transform.LookAt(target);
        

    }

    IEnumerator OrbitAround(int caseNum)
    {
        isOrbiting = true;
        Time.timeScale = 0;
        float targetAngle;
        float starttime = Time.realtimeSinceStartup;
        if(caseNum == 0)
        {
            targetAngle = angle + 90.0f;  // 목표 각도 설정
            float rotProgress = 0;
            while (angle < targetAngle)
            {
                
                angle = angle + (Time.realtimeSinceStartup-starttime) * orbitSpeed;  // 회전할 각도 계산
                

                if (angle > targetAngle)
                {
                    angle = targetAngle;  // 목표 각도를 초과하지 않도록 설정
                }

                //float radians = angle * Mathf.Deg2Rad;  // 라디안으로 변환
                //transform.position = new Vector3(0, Mathf.Sin(radians) * distance, -Mathf.Cos(radians) * distance) + target.position;
                //transform.rotation = Quaternion.Euler(angle, 0, 0);
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
            targetAngle = angle + 90.0f;  // 목표 각도 설정
            float rotProgress = 0;
            while (angle < targetAngle)
            {
                
                angle = angle + (Time.realtimeSinceStartup-starttime) * orbitSpeed;  // 회전할 각도 계산        
                if (angle > targetAngle)
                {
                    angle = targetAngle;  // 목표 각도를 초과하지 않도록 설정
                }
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
            targetAngle = angle + 90.0f;  // 목표 각도 설정
            float rotProgress = 0;
            while (angle < targetAngle)
            {
                
                angle = angle + (Time.realtimeSinceStartup-starttime) * orbitSpeed;  // 회전할 각도 계산        
                if (angle > targetAngle)
                {
                    angle = targetAngle;  // 목표 각도를 초과하지 않도록 설정
                }
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
            targetAngle = angle + 90.0f;  // 목표 각도 설정
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
        
        isOrbiting = false;
        Time.timeScale = 1;
    }
}
