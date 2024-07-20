using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallDoorOpen))]
public class WallOpenButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WallDoorOpen generator = (WallDoorOpen)target;
        if (GUILayout.Button("Door Active"))
        {
            generator.OpenDoor();
        }
    }
}
