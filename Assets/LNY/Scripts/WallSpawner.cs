using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    public GameObject[] Walls;
    public Transform center;
    public float distance = 100f;
    public int spawnedAlready = -1;

    public float spawnInterval = 10f;
    private float spawnPosition;
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
        Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
    }

    private int RandomGenerate()
    {
        int random = Random.Range(0, Walls.Length);

        while (random == spawnedAlready) 
        {
            random = Random.Range(0, Walls.Length);
            
        }

        spawnedAlready = random;
        return random;
    }
}
