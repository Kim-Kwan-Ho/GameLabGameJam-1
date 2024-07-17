using UnityEngine;


[RequireComponent(typeof(GameSceneEventArgs))]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public GameSceneEventArgs GameSceneEvent;
    public static int GameLevel = 1;

    [SerializeField] private GameObject _healthItem;
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
    }

    private void OnDestroy()
    {
        GameSceneEvent.EpicPatternEnd -= OnEpicPatternEnd;
    }

    private void OnEpicPatternEnd(GameSceneEventArgs gameSceneEventArgs)
    {
        GameLevel++;
        Instantiate(_healthItem, new Vector3(0, 10, 0), Quaternion.identity);
    }


}
