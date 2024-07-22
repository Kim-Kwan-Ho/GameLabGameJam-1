using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerAttack attackSystem;
    [SerializeField] private TextMeshProUGUI currentCoorText;
    [SerializeField] private TextMeshProUGUI goalCoorText;

    void Awake()
    {
        GameUI.Instance = this;
    }

    void Start()
    {
        currentCoorText.text = string.Format
        ("ÇöÀç ÁÂÇ¥ - ( x: {0}, y: {1}, z: {2} )",
            LevelManager.Instance.origin.x, LevelManager.Instance.origin.y, LevelManager.Instance.origin.z);
        UpdateGoalCoorText();
    }

    public void UpdateCurrentCoorText()
    {
        currentCoorText.text = string.Format
            ("ÇöÀç ÁÂÇ¥ - ( x: {0}, y: {1}, z: {2} )", 
                LevelManager.Instance.CurrentCoordinate.x, LevelManager.Instance.CurrentCoordinate.y, LevelManager.Instance.CurrentCoordinate.z);
    }

    public void UpdateGoalCoorText()
    {
        goalCoorText.text = string.Format
        ("Å»Ãâ±¸ - ( x: {0}, y: {1}, z: {2} )",
            LevelManager.Instance.goalPoint.x, LevelManager.Instance.goalPoint.y, LevelManager.Instance.goalPoint.z);
    }
}
