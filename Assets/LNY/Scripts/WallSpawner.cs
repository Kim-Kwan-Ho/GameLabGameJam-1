using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject[] Walls;
    public Transform center;
    public float distance = 100f;
    public int spawnedAlready = -1;

    public float spawnInterval = 10f;
    private float spawnPosition;

    private Queue<Transform> wallQueue = new Queue<Transform>();
    public PlayerRayCast playerRayCast;
    public void Start()
    {
        InvokeRepeating(nameof(WallSpawning), 0.5f, spawnInterval);
    }

    public void Update()
    {
        
    }

    void  WallSpawning()
    {
        GameObject wallPrefab = Walls[RandomGenerate()];
        Vector3 spawnPosition = center.position + new Vector3(0.67f, -5, distance);
        GameObject newWall = Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
        Transform wallTransform = newWall.transform;
        wallQueue.Enqueue(wallTransform);

        playerRayCast.UpdateWallTransform(wallTransform);

    }

    private int RandomGenerate()
    {
        int random = Random.Range(0, Walls.Length);

        if(Walls.Length == 1)
        {
            return random;
        }

        else
        {
            while (random == spawnedAlready)
            {
                random = Random.Range(0, Walls.Length);

            }

            spawnedAlready = random;
            return random;
        }
        
    }
}
