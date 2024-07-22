using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_Move : SubPattern
{
    [SerializeField] private PatternWarning warning;
    [SerializeField] private GameObject danger;
    [SerializeField] private Transform destination;
    private Transform dangerTF;
    private MeshRenderer dangerMesh;
    private Transform tf;

    public Transform PlayerTF;

    public float speed = 0;
    public float waitTime = 1.5f;

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
        warning.PlayWarning();

        yield return new WaitForSeconds(waitTime);

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

        while (Vector3.Distance(dangerTF.position, destination.position) > 0.1f) 
        {
            dangerTF.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
            yield return null;
        }

        while (alpha > 0)
        {
            dangerMesh.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.01f);
            alpha -= 0.05f;
        }

        danger.SetActive(false);
    }
}
