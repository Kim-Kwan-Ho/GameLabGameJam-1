using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Items")]
    [SerializeField] private GameObject[] _items;

    [Header("Spawn Settings")]
    [SerializeField] private Vector3 _centerPos = new Vector3(0, 10, 0);
    [SerializeField] private float _mid = 9.5f;
    [SerializeField] private float _spawnNLifeTime = 15;
    private float _coolTime = 0;

    private void Awake()
    {
        _coolTime = _spawnNLifeTime;
    }

    private void Update()
    {
        if (_coolTime > 0)
        {
            _coolTime -= Time.deltaTime;
            return;
        }
        SpawnItem();

    }


    private void SpawnItem()
    {
        _coolTime = _spawnNLifeTime;
        int ranI = Random.Range(0, _items.Length);
        float ranX = Random.Range(-_mid, _mid);
        float ranY = Random.Range(-_mid, _mid);
        float ranZ = Random.Range(-_mid, _mid);

        Items item = Instantiate(_items[ranI], new Vector3(_centerPos.x - ranX, _centerPos.y - ranY, _centerPos.z - ranZ), Quaternion.Euler(Vector3.up)).GetComponent<Items>();
        item.SetItem(_spawnNLifeTime);
    }





}
