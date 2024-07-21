using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;
using Random = UnityEngine.Random;

public class MapCreator : MonoBehaviour
{
    public MapCreateEventArgs MapCreateEvent;

    [SerializeField] private int[] roomCodeQueue = new int[20];

    private int mapSize;
    private bool isValidMap = false;

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

            // 원점(2), 골인지점(1) 지정
            SetOriginRoom(_origin);
            SetGoalRoom(_goal);

            // 맵 랜덤 생성 후 타당성 확인
            RandomizeMap();
            ValidationMap(_origin, _goal);
        }
        
        MapCreateEvent.CallMapCreateComplete();
        Debug.Log($"Map Creator: Map Randomize Completed  (Map size: {_mapSize}, Room count: {(int)Mathf.Pow(_mapSize, 3)})");
    }

    public bool CheckMapCreation()
    {
        return isValidMap;
    }

    private void SetOriginRoom(Vector3Int _origin)
    {
        // 원점을 평화 맵으로 지정 = 2
        LevelManager.Instance.levelMap[_origin.x][_origin.y][_origin.z] = 2;
        Debug.Log($"Map Creator: Set Origin to 1. (Origin: [{_origin.x}, {_origin.y}, {_origin.z}])");
    }

    private void SetGoalRoom(Vector3Int _goal)
    {
        // 골인 지점 지정 = 1
        LevelManager.Instance.levelMap[_goal.x][_goal.y][_goal.z] = 1;
        Debug.Log(($"Map Creator: Set goal point to 0. (Goal point: [{_goal.x}][{_goal.y}][{_goal.z}])"));
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
                }
            }
        }
        Debug.Log("Map Creator: Randomize Map Code");
    }

    private void ResetMap()
    {
        Debug.Log("Map Creator: Reset the map to -1");
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
        for (int i = 0; i < roomCodeQueue.Length; i++)
            roomRandomizeQueue.Add(roomCodeQueue[i]);
    }

    private void ValidationMap(Vector3Int _origin, Vector3 _goal)
    {
        // BFS를 이용하여 골인 지점까지 갈 수 있는지 탐색

        bool[,,] checkRoad = null;  // 들른 노드인지 확인

        // checkRoad false로 초기화
        checkRoad = new bool[mapSize, mapSize, mapSize];
        for (int i0 = 0; i0 < mapSize; i0++)
            for (int i1 = 0; i1 < mapSize; i1++)
                for (int i2 = 0; i2 < mapSize; i2++)
                    checkRoad[i0, i1, i2] = false;

        BFSNode bestNode = null;

        Queue<BFSNode> queue = new Queue<BFSNode>();
        queue.Enqueue(new BFSNode(_origin.x, _origin.y, _origin.z, null));
        checkRoad[_origin.x, _origin.y, _origin.z] = true;

        // 탐색할 것이 없을 때까지 루프
        while (queue.Count > 0)
        {
            // 노드를 가져옴
            BFSNode node = queue.Dequeue();

            // 목표 지점에 도달 시
            if (node.X == _goal.x && node.Y == _goal.y && node.Z == _goal.z)
            {
                isValidMap = true;

                break;
            }

            for (int i = 0; i < direction.GetLength(1); i++)
            {
                // 모든 방향으로 노드 탐색
                int dx = node.X + direction[0, i, 0];
                int dy = node.Y + direction[0, i, 1];
                int dz = node.Z + direction[0, i, 2];

                // 노드가 맵 내에 존재, 노드가 갈 수 있는 노드, 한 번도 간 적 없는 노드인 경우
                if (CheckMapRange(dx, dy, dz) && CheckMapWay(dx, dy, dz) && !checkRoad[dx, dy, dz])
                {
                    // 찾은 길에 대해서 노드를 만들어 Queue에 추가, 현재 노드는 찾은 노드의 이전 노드
                    BFSNode searchNode = new BFSNode(dx, dy, dz, node);
                    queue.Enqueue(searchNode);

                    // 이미 들른 노드로 체크
                    checkRoad[dx, dy, dz] = true;
                }
            }
        }

        if (isValidMap)
            Debug.Log("Map Creator: Valid Map Generated.");
        else
            Debug.Log("Map Creator: Invalid Map. Regenerate Random Map.");

        /*if (isValidMap)
        {
            Debug.Log($"[{bestNode.X}, {bestNode.Y}, {bestNode.Z}] = {LevelManager.Instance.levelMap[bestNode.X][bestNode.Y][bestNode.Z]}");
            while (isValidMap && bestNode.PrevCount > 0)
            {
                bestNode = bestNode.PrevNode;
                Debug.Log($"[{bestNode.X}, {bestNode.Y}, {bestNode.Z}] = {LevelManager.Instance.levelMap[bestNode.X][bestNode.Y][bestNode.Z]}");
            }
        }*/
    }

    private bool CheckMapRange(int x, int y, int z)
    {
        // 노드가 맵 내에 존재하는가
        return (x >= 0 && x < mapSize &&
                y >= 0 && y < mapSize &&
                z >= 0 && z < mapSize);
    }

    private bool CheckMapWay(int x, int y, int z)
    {
        // 갈 수 있는 노드인가
        return LevelManager.Instance.levelMap[x][y][z] != 0;
    }

    public class BFSNode
    {
        public int X;
        public int Y;
        public int Z;

        public BFSNode PrevNode;
        public int PrevCount;

        public BFSNode(int x, int y, int z, BFSNode prevNode)
        {
            X = x;
            Y = y;
            Z = z;
            PrevNode = prevNode;

            if (PrevNode == null)
            {
                // 이전 노드가 없으면 시작 지점이므로 Count는 0
                PrevCount = 0;
            }
            else
            {
                // 이전 노드가 있으면 이전 노드의 '이전 노드 개수' + 1
                // 목표지점에 해당하는 노드는 최종적으로 시작지점에서 목표지점까지의 노드 수가 담기게 된다
                PrevCount = PrevNode.PrevCount + 1;
            }
        }
    }
}
