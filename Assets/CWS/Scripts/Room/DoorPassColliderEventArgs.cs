using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassColliderEvent : MonoBehaviour
{
    public Action<DoorPassColliderEvent> DoorPass;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CallDoorPass();
        }
    }

    private void CallDoorPass()
    {
        DoorPass?.Invoke(this);
    }
}
