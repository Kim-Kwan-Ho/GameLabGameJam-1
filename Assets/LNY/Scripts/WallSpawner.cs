using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    public GameObject[] Walls;
    public Transform center;
    public float distance = 100f;

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
        GameObject wallPrefab = Walls[Random.Range(0, Walls.Length)];
        Vector3 spawnPosition = center.position + new Vector3(0, -5, distance);
        Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
    }

}
