using UnityEngine;

public class NormalTrigger : MonoBehaviour
{
    public WallManager wallManager;
    private Material ChangeMaterial;
    private Renderer cubeRenderer;


    private void Start()
    {   
        cubeRenderer = GetComponent<Renderer>();
        ChangeMaterial = Resources.Load<Material>("LNY/Materials/Wall1");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            wallManager.OnNormalTriggerActivated();
            
            cubeRenderer.material = ChangeMaterial;

        }
    }
}
