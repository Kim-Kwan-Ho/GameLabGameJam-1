using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;
using Random = UnityEngine.Random;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private int[] roomCodeQueue = new int[20];

    private int mapSize;
    private bool isValidMap = false;
    private int validRoomCount = 0;

    private List<List<List<int>>> map = new List<List<List<int>>>();
    private List<int> roomRandomizeQueue = new List<int>();

    public int[,,] direction = new int[,,]
    {
        {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 },
            { -1, 0, 0 },
            { 0, -1, 0 },
            { 0, 0, -1 }
        }
    };
    
    /* -1: 미지정
        0: 공백
        1: 골인지점
        2: 평화
        3: 쉬움
        4: 중간
        5: 어려움
        6: 극악
        7: 특수   */

    public void GetRandomMap(int _mapSize, Vector3Int _origin, Vector3Int _goal)
    {
        mapSize = _mapSize;
        
        while (!isValidMap)
        {
            // 맵을 -1로 초기화
            ResetMap();
            validRoomCount = 0;

            // 원점(2), 골인지점(1) 지정
            SetOriginRoom(_origin);
            SetGoalRoom(_goal);

            // 맵 랜덤 생성 후 타당성 확인
            RandomizeMap();
            ValidationMap();
        }
        
        Debug.Log($"Map Randomized (Map size: {_mapSize}, Room count: {(int)Mathf.Pow(_mapSize, 3)})");
    }

    private void SetOriginRoom(Vector3Int _origin)
    {
        // 원점을 평화 맵으로 지정 = 2
        LevelManager.Instance.levelMap[_origin.x][_origin.y][_origin.z] = 2;
        Debug.Log($"Set Origin to 1. (Origin: [{_origin.x}, {_origin.y}, {_origin.z}])");
    }

    private void SetGoalRoom(Vector3Int _goal)
    {
        // 골인 지점 지정 = 1
        LevelManager.Instance.levelMap[_goal.x][_goal.y][_goal.z] = 1;
        Debug.Log(($"Set goal point to 0. (Goal point: [{_goal.x}][{_goal.y}][{_goal.z}])"));
    }

    private void RandomizeMap()
    {
        // 미지정된 방 랜덤 지정
        for (int i0 = 0; i0 < mapSize; i0++)
        {
            for (int i1 = 0; i1 < mapSize; i1++)
            {
                for (int i2 = 0; i2 < mapSize; i2++)
                {
                    if (LevelManager.Instance.levelMap[i0][i1][i2] == -1)
                        LevelManager.Instance.levelMap[i0][i1][i2] = GetRandomRoom();

                    if (LevelManager.Instance.levelMap[i0][i1][i2] != 0)
                        validRoomCount++;
                }
            }
        }
    }

    private void ResetMap()
    {
        // -1로 맵 초기화
        for (int i0 = 0; i0 < mapSize; i0++)
        {
            for (int i1 = 0; i1 < mapSize; i1++)
            {
                for (int i2 = 0; i2 < mapSize; i2++)
                {
                    LevelManager.Instance.levelMap[i0][i1][i2] = -1;
                }
            }
        }
    }

    private void ValidationMap()
    {
        // BFS를 이용하여 골인 지점까지 갈 수 있는지 탐색

        bool[,,] checkRoad = null;  // 들른 노드인지 확인

        // checkRoad false로 초기화
        checkRoad = new bool[mapSize, mapSize, mapSize];
        for (int i0 = 0; i0 < mapSize; i0++)
            for (int i1 = 0; i1 < mapSize; i1++)
                for (int i2 = 0; i2 < mapSize; i2++)
                    checkRoad[i0, i1, i2] = false;


    }

    private int GetRandomRoom()
    {
        // 랜덤 큐가 비어있으면 리셋으로 보충
        if (roomRandomizeQueue.Count <= 0)
            ResetRandomizeQueue();
        
        // 방 큐로부터 랜덤한 방 뽑기
        int randomIndex = Random.Range(0, roomRandomizeQueue.Count);
        int result = roomRandomizeQueue[randomIndex];
        roomRandomizeQueue.RemoveAt(randomIndex);

        return result;
    }

    private void ResetRandomizeQueue()
    {
        // 방 큐 리셋
        for (int i = 0; i < roomCodeQueue.Length;i++)
            roomRandomizeQueue.Add(roomCodeQueue[i]);
    }
}
