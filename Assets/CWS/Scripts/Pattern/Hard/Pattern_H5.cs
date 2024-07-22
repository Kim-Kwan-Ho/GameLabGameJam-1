using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_H5: MonoBehaviour
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
            subPatternsTF[i].position = new Vector3(Center.position.x, Center.position.y, Random.Range(-9.5f, 9.5f));
            subPatterns[i].PlaySubPattern();
            yield return new WaitForSeconds(0.3f);
            subPatternsTF[i + 1].position = new Vector3(Center.position.x, Center.position.y, PlayerTF.position.z);
            subPatterns[i + 1].PlaySubPattern();
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(10f);


        Destroy(this.gameObject);
    }
}
