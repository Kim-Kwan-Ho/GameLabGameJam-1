using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Hell2 : MonoBehaviour
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

        for (int i = 0; i < subPatterns.Length; i += 2)
        {
            subPatternsTF[i].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
            subPatterns[i].PlaySubPattern();
            subPatternsTF[i + 1].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
            subPatterns[i + 1].PlaySubPattern();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(7f);


        Destroy(this.gameObject);
    }
}
