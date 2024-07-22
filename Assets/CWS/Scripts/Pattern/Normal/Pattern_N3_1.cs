using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_N3_1: MonoBehaviour
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

        subPatternsTF[0].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
        subPatterns[0].PlaySubPattern();
        yield return new WaitForSeconds(5f);
        subPatternsTF[1].position = new Vector3(Center.position.x, Center.position.y, PlayerTF.position.z);
        subPatterns[1].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[2].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
        subPatterns[2].PlaySubPattern();
        yield return new WaitForSeconds(2f);

        subPatternsTF[3].position = new Vector3(Center.position.x, Center.position.y, PlayerTF.position.z);
        subPatterns[3].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[4].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
        subPatterns[4].PlaySubPattern();
        yield return new WaitForSeconds(2f);

        subPatternsTF[5].position = new Vector3(Center.position.x, Center.position.y, PlayerTF.position.z);
        subPatterns[5].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[6].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
        subPatterns[6].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[7].position = new Vector3(Center.position.x, Center.position.y, PlayerTF.position.z);
        subPatterns[7].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[8].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
        subPatterns[8].PlaySubPattern();
        yield return new WaitForSeconds(1f);
        subPatternsTF[9].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
        subPatterns[9].PlaySubPattern();

        yield return new WaitForSeconds(6f);

        Destroy(this.gameObject);
    }
}
