using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public string SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        RankManager.Instance.ReadRankData();
        RankManager.Instance.UpdateRankUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            FadeManager.Instance.ChangeScene(SceneToLoad);
        }
    }
}
