using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSignal : MonoBehaviour
{
    [SerializeField] private GameObject[] _signalGobs;


    [SerializeField] private Vector3 _rotDirection;
    [SerializeField] private float _rotSpeed;
    private bool _enabled = false;
    [SerializeField] private float _signalTime = 2.0f;
    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal += StartWarningSignal;
    }

    private void OnDestroy()
    {

    }
    private void Update()
    {
        if (_enabled)
        {
            transform.Rotate(_rotDirection * _rotSpeed * Time.deltaTime);
        }
    }

    private void StartWarningSignal(GameSceneEventArgs gameSceneEventArgs)
    {
        StartCoroutine(StartSignal());
    }


    private IEnumerator StartSignal()
    {
        _enabled = true;
        foreach (var VARIABLE in _signalGobs)
        {
            VARIABLE.SetActive(true);
        }
        yield return new WaitForSeconds(_signalTime);
        EndWarningSignal();
    }


    private void EndWarningSignal()
    {
        _enabled = false;
        foreach (var VARIABLE in _signalGobs)
        {
            VARIABLE.SetActive(false);
        }
        GameSceneManager.Instance.GameSceneEvent.CallEpicPatternStart();
    }
}
