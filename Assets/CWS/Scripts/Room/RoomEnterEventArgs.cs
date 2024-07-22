using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterEventArgs : MonoBehaviour
{
    public Action<RoomEnterEventArgs> RoomEnterEvent;

    public void CallRoomEnter()
    {
        RoomEnterEvent?.Invoke(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (!LevelManager.Instance.GetCurrentRoomCleared())
                CallRoomEnter();
    }
}
