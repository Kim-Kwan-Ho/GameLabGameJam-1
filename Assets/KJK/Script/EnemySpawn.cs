using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   
    public Transform centerPosition; 
    public float spawnRadius = 30.0f; 
    public float spawnInterval = 1.0f;

    GameObject obj;

    void Start()
    {
        obj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/SampleEnemy.prefab", typeof(GameObject));
        InvokeRepeating(nameof(SpawnEnemy), 0.1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Generate a random point within a sphere around the center position
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        Debug.Log("The enemy has been spawned");
        randomDirection.y = Mathf.Abs(randomDirection.y); 

        Vector3 spawnPosition = centerPosition.position + randomDirection;

        
        
        Debug.Log(obj);
        Instantiate(obj, spawnPosition, Quaternion.identity);
    }
}
