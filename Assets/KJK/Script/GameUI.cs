using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerAttack attackSystem;

    //[SerializeField] private TextMeshProUGUI timerText; // 타이머 출력 text
    [SerializeField] private TextMeshProUGUI scoreText; // 스코어 출력 text

    [SerializeField] private GameObject[] ui_powerLevel;
    [SerializeField] private GameObject[] ui_speedLevel;
    [SerializeField] private GameObject[] ui_lifeLevel;

    void Update()
    {
        //DrawTimer();
        DrawScore();
        //LifeCheck(healthSystem.Health);
        PowerCheck(attackSystem.DamageLevel);
        SpeedCheck(attackSystem.AttackSpeedLevel);
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
    //public void LifeCheck(int lifeCount)
    //{
    //    // 현재 임시로 라이프값 3 입력함
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (i < lifeCount)
    //            ui_lifeLevel[i].SetActive(true);
    //        else
    //            ui_lifeLevel[i].SetActive(false);
    //    }
    //}

    // 파워
    public void PowerCheck(int powerLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < powerLevel)
                ui_powerLevel[i].SetActive(true);
            else
                ui_powerLevel[i].SetActive(false);
        }
    }

    // 스피드
    public void SpeedCheck(int speedLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < speedLevel)
                ui_speedLevel[i].SetActive(true);
            else
                ui_speedLevel[i].SetActive(false);
        }
    }

    // 스코어 출력
    private void DrawScore()
    {
        scoreText.text = string.Format("Score  {0:D6}", ScoreManager.instance.score);
    }

    private void RankCompare()
    {

    }
}
