using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternWarning : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private float waitTime = 0;

    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PlayWarning()
    {
        StartCoroutine(IE_PlayWarn());
    }

    IEnumerator IE_PlayWarn()
    {
        mesh.enabled = true;
        Color color = mesh.material.color;
        float savedAlpha = color.a;
        float alpha = 0f;

        while (alpha < savedAlpha)
        {
            mesh.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.01f);
            alpha += 0.05f;
        }

        alpha = savedAlpha;
        yield return new WaitForSeconds(waitTime);

        while (alpha > 0)
        {
            mesh.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.01f);
            alpha -= 0.05f;
        }

        mesh.enabled = false;
    }
}
