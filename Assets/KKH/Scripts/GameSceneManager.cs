using System.Collections;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(GameSceneEventArgs))]
public class GameSceneManager : MonoBehaviour
{
    public static WallSpawner wallSpawner;
    public static GameSceneManager Instance;
    public GameSceneEventArgs GameSceneEvent;
    public static int GameLevel = 1;

    [SerializeField] private GameObject _healthItem;


    [SerializeField] private float _stageTime = 15f;
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

        GameSceneEvent.GameResume += StartNextStageTimer;
        StartCoroutine(CoStartDelay());
    }
    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSeconds(Constants.GAME_STARTDELAY);
        StartCoroutine(StartNextStage());
    }
    private void OnDestroy()
    {
        GameSceneEvent.GameResume -= StartNextStageTimer;
    }

    private void Update()
    {
        _levelText.text = "STAGE " + GameLevel.ToString();
    }

    private void StartNextStageTimer(GameSceneEventArgs gameSceneEventArgs)
    {
        StartCoroutine(StartNextStage());
    }

    private IEnumerator StartNextStage()
    {
        yield return new WaitForSeconds(_stageTime);
        GameSceneEvent.CallWarningSignal();
        GameLevel++;
        
    }
}
