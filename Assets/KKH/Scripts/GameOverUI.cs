using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerScore;
    [SerializeField] private InputField _nameInput;
    [SerializeField] private GameObject _saveButton;

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
        RankManager.Instance.UpdateRankUI();

        _playerScore.text = string.Format("{0:D6}", (int)ScoreManager.instance.score);

        this.GetComponent<Canvas>().enabled = true;
    }

    public void SavePlayerRank()
    {
        RankManager.Instance.CompareRankScore(_nameInput.text, ScoreManager.instance.score);
        RankManager.Instance.WriteRankData();
        RankManager.Instance.UpdateRankUI();
        _saveButton.SetActive(false);
    }

    public void SceneMoveToTitle()
    {
        //SceneManager.LoadScene("TitleScene");
        FadeManager.Instance.ChangeScene("TitleScene");
    }
    public void SceneRestart()
    {
        GameSceneManager.GameLevel = 1;
        FadeManager.Instance.ChangeScene("GameScene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
