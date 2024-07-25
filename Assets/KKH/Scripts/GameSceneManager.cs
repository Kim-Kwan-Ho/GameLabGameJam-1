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

        // Remove the event subscription for the EpicPatternEnd and GameResume events
        // GameSceneEvent.EpicPatternEnd += OnEpicPatternEnd;
        // GameSceneEvent.GameResume += StartEpicPatternTimer;

        // Ensure that StartEpicPattern coroutine is not started
        // StartCoroutine(CoStartDelay());
    }

    private void Update()
    {
        //_levelText.text = "STAGE " + GameLevel.ToString();
    }

    // Removed OnEpicPatternEnd method

    // Removed StartEpicPatternTimer method

    // Removed StartEpicPattern coroutine

    // Removed CoStartDelay coroutine
}