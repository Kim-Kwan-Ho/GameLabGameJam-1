using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Hell1 : MonoBehaviour
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

        for (int i = 0; i < 5; i++)
        {
            subPatternsTF[i].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
            subPatterns[i].PlaySubPattern();
        }

        yield return new WaitForSeconds(2f);

        for (int i = 5; i < 10; i++)
        {
            subPatternsTF[i].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
            subPatterns[i].PlaySubPattern();
        }

        yield return new WaitForSeconds(2f);

        for (int i = 10; i < 15; i++)
        {
            subPatternsTF[i].position = new Vector3(Center.position.x, Center.position.y, Center.position.z);
            subPatterns[i].PlaySubPattern();
        }

        yield return new WaitForSeconds(10f);


        Destroy(this.gameObject);
    }
}
