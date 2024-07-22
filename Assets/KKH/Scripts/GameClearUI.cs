using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.GameClear += OnGameClear;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.GameClear -= OnGameClear;
    }


    private void OnGameClear(GameSceneEventArgs gameSceneEventArgs)
    {
        this.GetComponent<Canvas>().enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SceneRestart()
    {
        GameSceneManager.GameLevel = 1;
        Time.timeScale = 1.0f;
        FadeManager.Instance.ChangeScene("GameTest");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
