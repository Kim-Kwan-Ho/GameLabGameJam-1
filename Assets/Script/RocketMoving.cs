using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoving : MonoBehaviour
{
    public Transform target;
    private bool lockIn = false;
    private bool fire = false;

    // Update is called once per frame
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if(!lockIn)
        {
            transform.LookAt(target.transform);
            StartCoroutine(lockInTarget());
        }
        if(fire)
        {
            transform.Translate(Vector3.forward * 30 * Time.deltaTime);
        }
    }

    IEnumerator lockInTarget()
    {
        yield return new WaitForSeconds(1.0f);
        lockIn = true;
        yield return new WaitForSeconds(1.0f);
        fire = true;
    }
}
