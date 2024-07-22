using UnityEngine;

public class SolutionTrigger : MonoBehaviour
{
    public WallManager wallManager;
    private Material ChangeMaterial;
    private Renderer cubeRenderer;

    private void Start()
    {
        ChangeMaterial = Resources.Load<Material>("Assets/LNY/Materials/Wall.mat");
        cubeRenderer = gameObject.GetComponentInParent<Renderer>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            wallManager.OnSolutionTriggerActivated();
            cubeRenderer.material = ChangeMaterial;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wallManager.OnSolutionTriggerDeactivated();
        }
    }
}
