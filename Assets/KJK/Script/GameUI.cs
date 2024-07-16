using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private TextMeshProUGUI timerText; // 타이머 출력 text
    [SerializeField] private TextMeshProUGUI scoreText; // 스코어 출력 text


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawTimer();
        DrawScore();
    }

    // 타이머 출력
    private void DrawTimer()
    { 
        int timerMin = (int)scoreManager.currentTime / 60;
        int timerSec = (int)scoreManager.currentTime % 60;
        int timerMilsec = (int)(scoreManager.currentTime % 1 * 100);

        timerText.text = string.Format("Timer  {0:D2}:{1:D2}.{2:D2}", timerMin, timerSec, timerMilsec);
    }

    // 스코어 출력
    private void DrawScore()
    {
        scoreText.text = string.Format("Score  {0:D6}", scoreManager.score);
    }
}
