using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private WallDoorOpen[] walls = new WallDoorOpen[6];
    [SerializeField] private bool[] isLockedWalls = new bool[6];
    [SerializeField] private int[] roomCoordinate = new int[3];

    void Start()
    {
        CheckLockedDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    void Update()
    {
        
    }

    private void CheckLockedDoor()
    {
        // 좌표를 기반으로 하여 잠긴 문을 확인
        for (int i = 0; i < 6; i++)
            isLockedWalls[i] = false;

        if (roomCoordinate[0] == 0)
            isLockedWalls[3] = true;
        else if (roomCoordinate[0] == LevelManager.Instance.MapSize)
            isLockedWalls[0] = true;

        if (roomCoordinate[1] == 0)
            isLockedWalls[4] = true;
        else if (roomCoordinate[1] == LevelManager.Instance.MapSize)
            isLockedWalls[1] = true;

        if (roomCoordinate[2] == 0)
            isLockedWalls[5] = true;
        else if (roomCoordinate[2] == LevelManager.Instance.MapSize)
            isLockedWalls[2] = true;
    }

    public void DoorOpen()
    {
        for (int i = 0; i < 6; i++)
        {
            if (!isLockedWalls[i])
                walls[i].OpenDoor();
        }
    }

    public void DoorOpen(bool _isOpen)
    {
        for (int i = 0; i < 6; i++)
        {
            if (!isLockedWalls[i])
                walls[i].OpenDoor(_isOpen);
        }
    }

    private void RoomPatternStart()
    {

    }
}
