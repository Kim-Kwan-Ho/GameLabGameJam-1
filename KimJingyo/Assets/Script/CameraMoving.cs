using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour
{
    public Transform target;  // 카메라가 바라볼 중심점
    public float distance = 10.0f;  // 중심점에서의 거리
    public float orbitSpeed = 10.0f;  // 카메라가 회전하는 속도

    private float angle = 0.0f;  // 현재의 각도
    private bool isOrbiting = false;  // 코루틴 실행 중 여부 확인용 변수

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
            enabled = false;
            return;
        }

        // 초기 각도 설정
        this.transform.position = new Vector3(0, target.position.y, target.position.z - distance);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isOrbiting)
        {
            StartCoroutine(OrbitAround());
        }

        transform.LookAt(target);
        

    }

    IEnumerator OrbitAround()
    {
        isOrbiting = true;
        Time.timeScale = 0;
        float targetAngle;
        float starttime = Time.realtimeSinceStartup;
        if(angle == 0.0f)
        {
            targetAngle = angle + 90.0f;  // 목표 각도 설정
            
            while (angle < targetAngle)
            {
                
                angle = (Time.realtimeSinceStartup-starttime) * orbitSpeed;  // 회전할 각도 계산
                

                if (angle > targetAngle)
                {
                    angle = targetAngle;  // 목표 각도를 초과하지 않도록 설정
                }

                float radians = angle * Mathf.Deg2Rad;  // 라디안으로 변환
                transform.position = new Vector3(0, Mathf.Sin(radians) * distance, -Mathf.Cos(radians) * distance) + target.position;
                yield return null;
            }
        }
        else
        {
            targetAngle = angle - 90.0f;  // 목표 각도 설정
            while (angle > targetAngle)
            {
                angle = 90 - (Time.realtimeSinceStartup-starttime) * orbitSpeed;

                if (angle < targetAngle)
                {
                    angle = targetAngle;  // 목표 각도를 초과하지 않도록 설정
                }

                float radians = angle * Mathf.Deg2Rad;  // 라디안으로 변환
                transform.position = new Vector3(0, Mathf.Sin(radians) * distance, -Mathf.Cos(radians) * distance) + target.position;

                yield return null;
            }
        }
        if(PlayerMoving.view == PlayerMoving.viewState.TOP)
        {
            PlayerMoving.view = PlayerMoving.viewState.SIDE;
        }
        else
        {
            PlayerMoving.view = PlayerMoving.viewState.TOP;
        }
        isOrbiting = false;
        Time.timeScale = 1;
    }
}
