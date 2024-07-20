using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int MapSize { get { return mapSize; } }
    [SerializeField] int mapSize = 4;
    private List<List<List<int>>> levelMap = new List<List<List<int>>>();

    void Awake()
    {
        LevelManager.Instance = this;
    }

    void Start()
    {
        for (int i0 = 0; i0 < mapSize; i0++)
        {
            levelMap.Add(new List<List<int>>());
            for (int i1 = 0; i1 < mapSize; i1++)
            {
                levelMap[i0].Add(new List<int>());
                for (int i2 = 0; i2 < mapSize; i2++)
                    levelMap[i0][i1].Add(0);
            }
        }

    }

    void Update()
    {
        
    }

    [Serializable]
    public class Room
    {
        public int weight;
        public RoomCode code;
        
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
