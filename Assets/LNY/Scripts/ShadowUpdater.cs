using UnityEngine;

public class ShadowUpdater : MonoBehaviour
{
    public Camera shadowCamera; // Assign the shadow camera
    public RenderTexture shadowTexture; // Assign the Render Texture

    void Start()
    {
        if (shadowCamera == null || shadowTexture == null)
        {
            Debug.LogError("Shadow Camera and Render Texture must be assigned.");
            return;
        }

        shadowCamera.targetTexture = shadowTexture;
    }

    void Update()
    {
        // Update the shadow camera position and orientation based on the main object
        shadowCamera.transform.position = transform.position + Vector3.up * 10f; // Adjust the height as needed
        shadowCamera.transform.rotation = Quaternion.Euler(90, 0, 0); // Pointing downwards
    }
}