using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_H4_1 : MonoBehaviour
{
    public Transform PlayerTF;
    public Transform Center;

    [SerializeField] private SubPattern[] subPatterns;
    [SerializeField] private Transform[] subPatternsTF;

    void Start()
    {
        PlayerTF = LevelManager.Instance.Player.GetComponent<Transform>();
        Center = LevelManager.Instance.GetNewCenter();
        PlayPattern();
    }

    public void PlayPattern()
    {
        StartCoroutine(IE_Pattern());
    }

    public void OnDestroy()
    {
        LevelManager.Instance.RoomClear();
        StopAllCoroutines();
    }

    private IEnumerator IE_Pattern()
    {
        yield return new WaitForSeconds(1f);

        subPatternsTF[0].position =
            new Vector3(Center.position.x, Center.position.y, Center.position.z);
        subPatterns[0].PlaySubPattern();
        subPatternsTF[1].position =
            new Vector3(Center.position.x, Center.position.y, Center.position.z);
        subPatterns[1].PlaySubPattern();
        subPatternsTF[2].position =
            new Vector3(Center.position.x, Center.position.y, Center.position.z);
        subPatterns[2].PlaySubPattern();
        subPatternsTF[3].position =
            new Vector3(Center.position.x, Center.position.y, Center.position.z);
        subPatterns[3].PlaySubPattern();

        yield return new WaitForSeconds(13f);


        Destroy(this.gameObject);
    }
}
