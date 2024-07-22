using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_MoveStopMove : SubPattern
{
    [SerializeField] private PatternWarning warning1;
    [SerializeField] private PatternWarning warning2;
    [SerializeField] private GameObject danger;
    [SerializeField] private Transform destination1;
    [SerializeField] private Transform destination2;
    private Transform dangerTF;
    private MeshRenderer dangerMesh;
    private Transform tf;

    public Transform PlayerTF;

    public float speed1 = 0;
    public float speed2 = 0;
    public float waitTime1 = 0f;
    public float waitTime2 = 0f;

    void Start()
    {
        PlayerTF = LevelManager.Instance.Player.GetComponent<Transform>();
        tf = GetComponent<Transform>();
        dangerTF = danger.GetComponent<Transform>();
        dangerMesh = danger.GetComponent<MeshRenderer>();
        danger.SetActive(false);
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public override void PlaySubPattern()
    {
        StartCoroutine(IE_PlayPattern());
    }

    IEnumerator IE_PlayPattern()
    {
        warning1.PlayWarning();

        yield return new WaitForSeconds(1.5f);

        danger.SetActive(true);

        Color color = dangerMesh.material.color;
        float savedAlpha = color.a;
        float alpha = 0f;

        while (alpha < savedAlpha)
        {
            dangerMesh.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.01f);
            alpha += 0.05f;
        }

        alpha = savedAlpha;

        while (Vector3.Distance(dangerTF.position, destination1.position) > 0.1f) 
        {
            dangerTF.Translate(Vector3.left * speed1 * Time.deltaTime, Space.Self);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime1);
        warning2.PlayWarning();
        yield return new WaitForSeconds(1.5f);

        while (Vector3.Distance(dangerTF.position, destination2.position) > 0.1f)
        {
            dangerTF.Translate(Vector3.left * speed2 * Time.deltaTime, Space.Self);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime2);

        while (alpha > 0)
        {
            dangerMesh.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.01f);
            alpha -= 0.05f;
        }

        danger.SetActive(false);
    }
}
