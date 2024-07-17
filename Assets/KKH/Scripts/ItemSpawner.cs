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
    private bool _canSpawn = false;

    private void Awake()
    {
        _coolTime = _spawnNLifeTime;
    }
    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal += OnWarningSignalStart;
        GameSceneManager.Instance.GameSceneEvent.GameOver += OnGameOver;
        GameSceneManager.Instance.GameSceneEvent.GameResume += OnGameResume;
        StartCoroutine(CoStartDelay());
    }

    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSeconds(Constants.GAME_STARTDELAY);
        _canSpawn = true;
    }
    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal -= OnWarningSignalStart;
        GameSceneManager.Instance.GameSceneEvent.GameOver -= OnGameOver;
        GameSceneManager.Instance.GameSceneEvent.GameResume -= OnGameResume;
    }

    private void OnWarningSignalStart(GameSceneEventArgs gameSceneEventArgs)
    {
        _canSpawn = false;
    }
    private void OnGameOver(GameSceneEventArgs gameSceneEventArgs)
    {
        _canSpawn = false;
    }
    private void OnGameResume(GameSceneEventArgs gameSceneEventArgs)
    {
        _canSpawn = true;
    }
    private void Update()
    {
        if (!_canSpawn)
            return;
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
