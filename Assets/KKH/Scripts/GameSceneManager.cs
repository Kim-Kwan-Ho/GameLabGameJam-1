using System.Collections;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(GameSceneEventArgs))]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public GameSceneEventArgs GameSceneEvent;
    public static int GameLevel = 1;

    [SerializeField] private GameObject _healthItem;


    [SerializeField] private float _epicPatternTime = 15f;
    [SerializeField] private TextMeshProUGUI _levelText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        GameSceneEvent = GetComponent<GameSceneEventArgs>();


        GameSceneEvent.EpicPatternEnd += OnEpicPatternEnd;
        GameSceneEvent.GameResume += StartEpicPatternTimer;
        StartCoroutine(CoStartDelay());
    }
    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSeconds(Constants.GAME_STARTDELAY);
        StartCoroutine(StartEpicPattern());
    }
    private void OnDestroy()
    {
        GameSceneEvent.EpicPatternEnd -= OnEpicPatternEnd;
        GameSceneEvent.GameResume -= StartEpicPatternTimer;
    }

    private void OnEpicPatternEnd(GameSceneEventArgs gameSceneEventArgs)
    {
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
