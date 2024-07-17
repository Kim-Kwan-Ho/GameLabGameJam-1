using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoving : MonoBehaviour
{
    public Transform target;
    private bool lockIn = false;
    private bool fire = false;

    // Update is called once per frame
    private void OnEnable()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal += OnWarningSignalStart;
    }

    private void OnDisable()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal -= OnWarningSignalStart;
    }

    private void OnWarningSignalStart(GameSceneEventArgs gameSceneEventArgs)
    {
        //Instantiate(_enemyDeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
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

        if(transform.position.y > 40 || transform.position.y < -20 || transform.position.z > 30 || transform.position.z < -30 || transform.position.x > 30 || transform.position.x < -30)
        {
            Destroy(gameObject);
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
