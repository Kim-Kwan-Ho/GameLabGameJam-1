using UnityEngine;


[RequireComponent(typeof(GameSceneEventArgs))]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    private GameState _gameState = GameState.Normal;
    public GameSceneEventArgs GameSceneEvent;



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
    }

}



public enum GameState
{
    Normal,
    Warning,
    EpicPattern,
    Reward,
    GameEnd,

}