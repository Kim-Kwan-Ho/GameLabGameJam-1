using System.Collections.Generic;
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
        spawnInterval -= (Constants.LEVEL_WALL_SPAWNTIME * GameSceneManager.GameLevel);
    }

    public void Update()
    {
        spawnInterval -= (Constants.LEVEL_WALL_SPAWNTIME * GameSceneManager.GameLevel);
    }

    


    void  WallSpawning()
    {
        GameObject wallPrefab = Walls[RandomGenerate()];
        Vector3 spawnPosition = center.position + new Vector3(0.67f, -5, distance);
        Instantiate(wallPrefab, spawnPosition, Quaternion.identity);

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
