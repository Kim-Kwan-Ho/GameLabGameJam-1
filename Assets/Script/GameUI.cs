using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    static GameUI instance;
    [SerializeField] private ScoreManager scoreManager;

    //[SerializeField] private TextMeshProUGUI timerText; // 타이머 출력 text
    [SerializeField] private TextMeshProUGUI scoreText; // 스코어 출력 text

    [SerializeField] private GameObject[] ui_powerLevel;
    [SerializeField] private GameObject[] ui_speedLevel;
    [SerializeField] private GameObject[] ui_lifeLevel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DrawTimer();
        DrawScore();
    }

    // 타이머 출력
    /*private void DrawTimer()
    { 
        int timerMin = (int)scoreManager.currentTime / 60;
        int timerSec = (int)scoreManager.currentTime % 60;
        int timerMilsec = (int)(scoreManager.currentTime % 1 * 100);

        timerText.text = string.Format("Timer  {0:D2}:{1:D2}.{2:D2}", timerMin, timerSec, timerMilsec);
    }*/

    // 라이프 체크
    public void LifeCheck(int lifeCount)
    {
        // 현재 임시로 라이프값 3 입력함
        for (int i = 0; i < 3; i++)
        {
            if (i < lifeCount)
                ui_lifeLevel[i - 1].SetActive(true);
            else
                ui_lifeLevel[i - 1].SetActive(false);
        }
    }

    // 파워
    public void PowerCheck(int powerLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < powerLevel)
                ui_powerLevel[i - 1].SetActive(true);
            else
                ui_powerLevel[i - 1].SetActive(false);
        }
    }

    // 스피드
    public void SpeedCheck(int speedLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < speedLevel)
                ui_speedLevel[i - 1].SetActive(true);
            else
                ui_speedLevel[i - 1].SetActive(false);
        }
    }

    // 스코어 출력
    private void DrawScore()
    {
        scoreText.text = string.Format("Score  {0:D6}", scoreManager.score);
    }
}
