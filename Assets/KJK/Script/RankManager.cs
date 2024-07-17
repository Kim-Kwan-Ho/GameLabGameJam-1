using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;

    public float[] bestScore = new float[8];      // 하이스코어 점수(7등까지)
    public string[] bestName = new string[8];     // 하이스코어 이름

    public InputField InputName;
    public InputField InputScore;

    public GameObject parentObject;
    public GameObject[] rankingInfos;
    public GameObject[] rankingChildren = new GameObject[3];

    private void Awake()
    {
        if (Instance == null)
            RankManager.Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Get Saved Rank
        ReadRankData();
    }

    void Update()
    {

    }

    // 랭킹 불러오기
    public void ReadRankData()
    {
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
                bestName[i] = "NAN";
            }
        }
    }

    // 랭킹 저장하기
    public void WriteRankData()
    {
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i + "BestName", bestName[i]);
        }

        //UpdateRankUI();
    }

    public void UpdateRankUI()
    {
        //fetch the info from bestScore and bestName arrays
        //access the children of GameObject -> access rank, name and score to update the ranking info

        rankingInfos = new GameObject[parentObject.transform.childCount];

        for(int i=0; i<7; i++)
        {
            rankingInfos[i] = parentObject.transform.GetChild(i).gameObject;
            for(int j=0; j<=2; j++)
            {
                rankingChildren[j] = rankingInfos[i].transform.GetChild(j).gameObject;

            }

            TextMeshProUGUI rankText = rankingChildren[0].GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = rankingChildren[1].GetComponent<TextMeshProUGUI>(); 
            TextMeshProUGUI scoreText = rankingChildren[2].GetComponent<TextMeshProUGUI>();
            
            rankText.text = (i + 1).ToString();
            nameText.text = bestName[i].ToString();
            scoreText.text = string.Format("{0:D6}", (int)bestScore[i]); 
        }

        
    }

    // 플레이어의 점수와 랭킹 비교 후 랭킹 정렬
    public void CompareRankScore(string name, float score)
    {
        float tmpScore;
        string tmpName;

        bestScore[7] = score;
        bestName[7] = name;

        for (int i = 7; i > 0; i--)
        {
            if (bestScore[i] > bestScore[i - 1])
            {
                tmpName = bestName[i - 1];
                tmpScore = bestScore[i - 1];

                bestName[i - 1] = bestName[i];
                bestScore[i - 1] = bestScore[i];

                bestName[i] = tmpName;
                bestScore[i] = tmpScore;
            }
        }
    }

    public void ResetData()
    {
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", 0);
            PlayerPrefs.SetString(i + "BestName", "NAN");
        }
    }

    public void test()
    {
        ReadRankData();
        CompareRankScore(InputName.text,float.Parse(InputScore.text));
        WriteRankData();
        UpdateRankUI();
    }
}