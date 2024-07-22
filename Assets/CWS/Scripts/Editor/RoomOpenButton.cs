using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomController))]
public class RoomOpenButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomController generator = (RoomController)target;

        if (GUILayout.Button("Door Active"))
        {
            generator.ActiveDoor();
        }

        if (GUILayout.Button("Room Clear"))
        {
            generator.RoomClear();
        }
    }
}