using System.Collections;
using UnityEngine;


[RequireComponent(typeof(GameSceneEventArgs))]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public GameSceneEventArgs GameSceneEvent;
    public static int GameLevel = 1;

    [SerializeField] private GameObject _healthItem;


    [SerializeField] private float _epicPatternTime = 15f;
    private void Awake()
    {
        GameSceneEvent = GetComponent<GameSceneEventArgs>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        GameSceneEvent.EpicPatternEnd += OnEpicPatternEnd;
        GameSceneEvent.GameResume += StartEpicPatternTimer;
        StartCoroutine(StartEpicPattern());
    }

    private void OnDestroy()
    {
        GameSceneEvent.EpicPatternEnd -= OnEpicPatternEnd;
        GameSceneEvent.GameResume -= StartEpicPatternTimer;
    }

    private void OnEpicPatternEnd(GameSceneEventArgs gameSceneEventArgs)
    {
        GameLevel++;
        Instantiate(_healthItem, new Vector3(0, 10, 0), Quaternion.identity);
    }

    private void StartEpicPatternTimer(GameSceneEventArgs gameSceneEventArgs)
    {
        StartCoroutine(StartEpicPattern());
    }
    private IEnumerator StartEpicPattern()
    {
        yield return new WaitForSeconds(_epicPatternTime);
        GameSceneEvent.CallWarningSignal();
    }
}
