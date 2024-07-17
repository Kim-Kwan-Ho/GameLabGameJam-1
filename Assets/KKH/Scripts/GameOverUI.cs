using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.GameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.GameOver -= OnGameOver;
    }


    private void OnGameOver(GameSceneEventArgs gameSceneEventArgs)
    {
        this.GetComponent<Canvas>().enabled = true;
    }
}
