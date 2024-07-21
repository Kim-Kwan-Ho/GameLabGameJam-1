using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private WallStruct[] wallStructs = new WallStruct[6];
    [SerializeField] private Vector3Int roomCoordinate = Vector3Int.zero;
    [SerializeField] private bool isClearedRoom = false;
    [SerializeField] private int roomCode = 0;
    
    public MeshRenderer FadeScreenMesh;
    public Transform Center;

    private BoxCollider roomPassCollider;

    void Awake()
    {
        roomPassCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        LevelManager.Instance.MapCreator.MapCreateEvent.MapCreateComplete += ApplyRoomState;

        if (LevelManager.Instance.MapCreator.CheckMapCreation())
            ApplyRoomState();
        else
            roomCode = 2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isClearedRoom)
            {
                Debug.Log("Enter not cleared room.");
                ActiveDoor(true);
            }
            else
            {
                Debug.Log("Enter cleared room.");
                roomPassCollider.enabled = false;
            }
        }
    }

    void OnDestroy()
    {
        LevelManager.Instance.MapCreator.MapCreateEvent.MapCreateComplete -= ApplyRoomState;
    }

    // 맵이 생성되어 있다면 호출 가능
    private void ApplyRoomState(MapCreateEventArgs mapCreateEventArgs)
    {
        roomCode = LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y][roomCoordinate.z];

        CheckLockedDoor();
        CloseLockedDoor();
    }
    private void ApplyRoomState()
    {
        roomCode = LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y][roomCoordinate.z];

        CheckLockedDoor();
        CloseLockedDoor();
    }

    private void CheckLockedDoor()
    {
        // 좌표를 기반으로 하여 잠긴 문을 확인
        for (int i = 0; i < 6; i++)
            wallStructs[i].isLockedWall = false;

        // X
        if (roomCoordinate.x <= 0)
            wallStructs[3].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x - 1][roomCoordinate.y][roomCoordinate.z] == 0)
            wallStructs[3].isLockedWall = true;

        if (roomCoordinate.x >= LevelManager.Instance.MapSize - 1)
            wallStructs[0].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x + 1][roomCoordinate.y][roomCoordinate.z] == 0)
            wallStructs[0].isLockedWall = true;

        // Y
        if (roomCoordinate.y <= 0)
            wallStructs[4].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y - 1][roomCoordinate.z] == 0)
            wallStructs[4].isLockedWall = true;

        if (roomCoordinate.y >= LevelManager.Instance.MapSize - 1)
            wallStructs[1].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y + 1][roomCoordinate.z] == 0)
            wallStructs[1].isLockedWall = true;

        // Z
        if (roomCoordinate.z <= 0)
            wallStructs[5].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y][roomCoordinate.z - 1] == 0)
            wallStructs[5].isLockedWall = true;

        if (roomCoordinate.z >= LevelManager.Instance.MapSize - 1)
            wallStructs[2].isLockedWall = true;
        else if (LevelManager.Instance.levelMap[roomCoordinate.x][roomCoordinate.y][roomCoordinate.z + 1] == 0)
            wallStructs[2].isLockedWall = true;

    }

    public Vector3Int GetCoordinate()
    {
        return roomCoordinate;
    }

    public void SetCoordinate(Vector3Int dir)
    {
        roomCoordinate += dir;
    }

    private void CloseLockedDoor()
    {
        for (int i = 0; i < 6; i++)
        {
            if (wallStructs[i].isLockedWall == true)
                wallStructs[i].wall.ActiveDoor(true);
            else
                wallStructs[i].wall.ActiveDoor(false);
        }
    }

    public void ActiveDoor()
    {
        for (int i = 0; i < 6; i++)
        {
            if (!wallStructs[i].isLockedWall)
                wallStructs[i].wall.ActiveDoor();
        }
    }

    public void ActiveDoor(bool _isOpen)
    {
        for (int i = 0; i < 6; i++)
        {
            if (!wallStructs[i].isLockedWall)
                wallStructs[i].wall.ActiveDoor(_isOpen);
        }
    }

    private void RoomPatternStart()
    {

    }

    [Serializable]
    public struct WallStruct
    {
        public WallDoorOpen wall;
        public bool isLockedWall;
    }
}
