using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameSceneManager.Instance.GameSceneEvent.CallWarningSignal();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameSceneManager.Instance.GameSceneEvent.CallOnGameResume();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GameSceneManager.Instance.GameSceneEvent.CallEpicPatternStart();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameSceneManager.Instance.GameSceneEvent.CallEpicPatternStart();
        }
    }
}
