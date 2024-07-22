using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.CurrentRoom.GetComponent<RoomController>().ActiveDoor(true);
        LevelManager.Instance.CallClearGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
