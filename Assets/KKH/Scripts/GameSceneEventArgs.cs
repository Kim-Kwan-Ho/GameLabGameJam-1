using System;
using UnityEngine;


public class GameSceneEventArgs : MonoBehaviour
{
    public Action<GameSceneEventArgs> GameResume;
    public Action<GameSceneEventArgs> WarningSignal;
    public Action<GameSceneEventArgs> EpicPatternStart;
    public Action<GameSceneEventArgs> GameOver;



    public void CallOnGameResume()
    {
        GameResume?.Invoke(this);
    }
    public void CallWarningSignal()
    {
        WarningSignal?.Invoke(this);
    }
    public void CallEpicPatternStart()
    {
        EpicPatternStart?.Invoke(this);
    }
    public void CallGameOver()
    {
        GameOver?.Invoke(this);
    }
}




