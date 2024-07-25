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

    private float timeSinceLastIntervalDecrease = 0f;
    //decrease by 0.2f every 5 seconds
    public float intervalDecreaseAmount = 0.2f;
    public float intervalDecreaseTime = 3f;
    public void Start()
    {
        InvokeRepeating(nameof(WallSpawning), 0.5f, spawnInterval);
    }

    public void Update()
    {
        // Update time tracker
        timeSinceLastIntervalDecrease += Time.deltaTime;

        // Check if it's time to decrease the spawn interval
        if (timeSinceLastIntervalDecrease >= intervalDecreaseTime)
        {
            // Decrease the spawn interval
            spawnInterval -= intervalDecreaseAmount;

            // Ensure spawnInterval does not go below a minimum value (optional)
            if (spawnInterval < 1f) // Adjust the minimum value as needed
            {
                spawnInterval = 1f; // You can set this to any minimum value
            }

            // Restart the InvokeRepeating with the updated spawn interval
            CancelInvoke(nameof(WallSpawning));
            InvokeRepeating(nameof(WallSpawning), 0.5f, spawnInterval);

            // Reset the time tracker
            timeSinceLastIntervalDecrease = 0f;
        }
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
