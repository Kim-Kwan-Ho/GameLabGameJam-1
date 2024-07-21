using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassColliderEventArgs : MonoBehaviour
{
    public Action<DoorPassColliderEventArgs> DoorPass;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Call Door Pass Event");
            CallDoorPass();
        }
    }

    private void CallDoorPass()
    {
        DoorPass?.Invoke(this);
    }
}
