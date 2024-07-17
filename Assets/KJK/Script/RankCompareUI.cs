using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankCompareUI : MonoBehaviour
{
    [SerializeField] private float[] bestScore = new float[7];
    [SerializeField] private string[] bestName = new string[7];

    [SerializeField] private int nextRank = 7;

    [SerializeField] private TextMeshProUGUI _nextRankNameText;
    [SerializeField] private TextMeshProUGUI _nextRankScoreText;

    void Start()
    {
        // Get Saved Rank
        for (int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.HasKey(i + "BestName"))
            {
                bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
                bestName[i] = PlayerPrefs.GetString(i + "BestName");
            }
            else
            {
                bestScore[i] = 0;
                bestName[i] = "none";
            }
        }
    }

    void Update()
    {
        GetNextRank();
        DrawNextRank();
    }

    private void GetNextRank()
    {
        for (int i = 6; i >= 0; i--)
        {
            if (ScoreManager.instance.score < bestScore[i])
                return;

            nextRank = i;
        }
    }

    private void DrawNextRank()
    {
        int rank = Math.Clamp(nextRank - 1, 0, bestName.Length - 1);
        _nextRankNameText.text = bestName[rank];
        _nextRankScoreText.text = string.Format("{0:D6}", (int)bestScore[rank]);
    }
}
