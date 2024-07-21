using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreateEventArgs : MonoBehaviour
{
    public Action<MapCreateEventArgs> MapCreateComplete;

    public void CallMapCreateComplete()
    {
        MapCreateComplete?.Invoke(this);
    }
}
