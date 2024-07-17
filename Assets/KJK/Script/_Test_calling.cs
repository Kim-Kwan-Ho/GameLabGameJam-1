using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Test_calling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameSceneManager.Instance.GameSceneEvent.CallOnGameStart();
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            GameSceneManager.Instance.GameSceneEvent.CallWarningSignal();
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            GameSceneManager.Instance.GameSceneEvent.CallEpicPatternStart();
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            GameSceneManager.Instance.GameSceneEvent.CallGameOver();
        }
    }
}
