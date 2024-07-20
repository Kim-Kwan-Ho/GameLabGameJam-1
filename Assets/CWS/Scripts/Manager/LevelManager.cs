using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private MapCreator MapCreator;

    public Transform RoomParentTransform;
    public CameraMoving CameraController;

    public GameObject PrevRoom;
    public GameObject CurrentRoom;

    public int MapSize { get { return mapSize; } }
    [SerializeField] private int mapSize = 3;
    [SerializeField] private Vector3Int origin = Vector3Int.zero;
    [SerializeField] private Vector3Int goalPoint = Vector3Int.zero;
    public List<List<List<int>>> levelMap = new List<List<List<int>>>();
    private List<List<List<bool>>> levelVisitedMap = new List<List<List<bool>>>();

    public Room[] roomList;


    void Awake()
    {
        LevelManager.Instance = this;
    }

    void Start()
    {
        GenerateMapList();
        MapCreator.GetRandomMap(mapSize, origin, goalPoint);
    }

    void Update()
    {
        
    }

    private void GenerateMapList()
    {
        for (int i0 = 0; i0 < mapSize; i0++)
        {
            levelMap.Add(new List<List<int>>());
            levelVisitedMap.Add(new List<List<bool>>());
            for (int i1 = 0; i1 < mapSize; i1++)
            {
                levelMap[i0].Add(new List<int>());
                levelVisitedMap[i0].Add(new List<bool>());
                for (int i2 = 0; i2 < mapSize; i2++)
                {
                    levelMap[i0][i1].Add(-1);
                    levelVisitedMap[i0][i1].Add(false);
                }
            }
        }

        Debug.Log("Generate the map List.");
    }

    public void MoveRoom(Vector3Int moveDir, GameObject newRoomObj, GameObject oldRoomObj)
    {
        RoomController newRoomController = newRoomObj.GetComponent<RoomController>();
        RoomController oldRoomController = oldRoomObj.GetComponent<RoomController>();

        newRoomController.SetCoordinate(oldRoomController.GetCoordinate() + moveDir);

        newRoomController.FadeScreenMesh.enabled = true;
        oldRoomController.FadeScreenMesh.enabled = true;

        PrevRoom = oldRoomObj;
        CurrentRoom = newRoomObj;

        StartCoroutine(IE_FadeRoom(newRoomController.FadeScreenMesh, oldRoomController.FadeScreenMesh));
        CameraController.MoveRoomCam(moveDir);
    }

    IEnumerator IE_FadeRoom(MeshRenderer newRoomMesh, MeshRenderer oldRoomMesh)
    {
        float fadeInAlpha = 1;
        float fadeOutAlpha = 0;

        while (fadeInAlpha >= 0)
        {
            newRoomMesh.material.color = new Color(0, 0, 0, fadeInAlpha);
            oldRoomMesh.material.color = new Color(0, 0, 0, fadeOutAlpha);

            fadeInAlpha = fadeInAlpha - 0.05f;
            fadeOutAlpha = fadeOutAlpha + 0.05f;

            yield return new WaitForSecondsRealtime(0.01f);
        }

        newRoomMesh.material.color = new Color(0, 0, 0, 0);
        oldRoomMesh.material.color = new Color(0, 0, 0, 1);

        yield break;
    }

    public void RemoveOldRoom()
    {
        Destroy(PrevRoom);
    }

    public void ResetRoomPos(Vector3 correctionVector)
    {
        // 방 좌표 이동
        CurrentRoom.transform.position += correctionVector;
        Debug.Log($"Reset room position: {correctionVector}");
    }

    public Vector3 GetCorrectionVector()
    {
        // Prev를 삭제하기 전에 사용하기(중요)
        return PrevRoom.transform.position - CurrentRoom.transform.position;
    }

    public Transform GetNewCenter()
    {
        return CurrentRoom.GetComponent<RoomController>().Center;
    }

    [Serializable]
    public class Room
    {
        public int weight;
        public RoomCode code;
        public GameObject prefab;
    }

    public enum RoomCode
    {
        None,
        Easy,
        Normal,
        Hard,
        VeryHard,
        
    }
}
