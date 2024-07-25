using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public bool startScoring = false;    // Ÿ�̸� �� ������ üũ�� ���ΰ�?
    public int score = 0;               // ��ü ���� (óġ�� �� �� 100��, ����ð� 1�� �� 100��)
    public float currentTime = 0f;      // ���� ����� �ð�
    public int killCount = 0;           // óġ�� ���� ��


    private float _timeScore;
    private int _itemScore = 0;
    private int _killScore = 0;
    void Awake()
    {
        ScoreManager.instance = this;
        startScoring = false;
    }

    void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.GameOver += StopScoring;
        GameSceneManager.Instance.GameSceneEvent.EpicPatternEnd += StopScoring;
        GameSceneManager.Instance.GameSceneEvent.GameResume += StartScoring;
        StartCoroutine(CoStartDelay());
    }

    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSeconds(Constants.GAME_STARTDELAY);
        startScoring = true;
    }
    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.GameOver -= StopScoring;
        GameSceneManager.Instance.GameSceneEvent.EpicPatternEnd -= StopScoring;
        GameSceneManager.Instance.GameSceneEvent.GameResume -= StartScoring;
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (startScoring)
        {
            currentTime += Time.deltaTime;
            score = ((int)currentTime * Constants.SCORE_TIME) + _killScore + _itemScore;
        }
    }


    public void IncreaseItemScore(int amount)
    {
        _itemScore += amount;
    }

    public void IncreaseKillScore(int amount)
    {
        _killScore += _killScore;
    }
    private void StopScoring(GameSceneEventArgs gameSceneEventArgs)
    {
        startScoring = false;
    }
    private void StartScoring(GameSceneEventArgs gameSceneEventArgs)
    {
        startScoring = true;
    }
}