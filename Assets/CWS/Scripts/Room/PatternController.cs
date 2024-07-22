using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField] private RoomEnterEventArgs roomEnterEvent;

    public GameObject[] PatternsList;

    void Start()
    {
        roomEnterEvent.RoomEnterEvent += PatternStart;
    }

    void OnDestroy()
    {
        roomEnterEvent.RoomEnterEvent -= PatternStart;
    }

    private void PatternStart(RoomEnterEventArgs roomEnterEventArgs)
    {
        GetComponent<BoxCollider>().enabled = false;
        if (PatternsList.Length > 0)
            PlayPattern(GetRandomPattern());
    }

    private int GetRandomPattern()
    {
        return Random.Range(0, PatternsList.Length);
    }

    private void PlayPattern(int patternIndex)
    {
        Debug.Log($"Play {patternIndex}th pattern");
        GameObject pattern = Instantiate(PatternsList[patternIndex]);
    }
}
