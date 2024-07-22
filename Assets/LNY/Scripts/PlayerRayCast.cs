using UnityEditor.TextCore.Text;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public Transform wallTransform;  // Reference to the moving wall's transform
    public LayerMask wallLayer;      // LayerMask to specify which layers are considered as walls
    public float rayLength = 10f;    // Length of the ray
    public float clearance = 1.0f;   // Minimum clearance required to pass through the wall
    public float moveSpeed = 5f;     // Speed at which the player will move to avoid the wall

    private BoxCollider[] colliders;  // Array to hold the player's BoxCollider components

    private void Start()
    {
        // Get all BoxCollider components from the child objects
        colliders = GetComponentsInChildren<BoxCollider>();
       
    }

    private void Update()
    {
        RayCasting();
    }

    private void RayCasting()
    {
        // Variable to keep track of if any of the rays hit the wall
        bool isCollisionDetected = false; 

        // Iterate through all colliders of the player
        foreach (BoxCollider collider in colliders)
        {
            // Get the bounds of the current collider
            Bounds colliderBounds = collider.bounds;

            // Get the corner positions of the collider bounds
            Vector3[] rayOrigins = GetBoundsCorners(colliderBounds);

            // Define the direction for straight raycasting (local -Z direction)
            Vector3 rayDirection = -transform.forward;  // -Z direction relative to the player's local space

            foreach (Vector3 origin in rayOrigins)
            {
                Ray ray = new Ray(origin, rayDirection);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, rayLength, wallLayer)) //cast a ray - if it hits the wall then returns true
                {
                     //만약 wallTransform이 null이 아니고 콜라이드 한 애가 wallTransform이라면 
        
                        if (hitInfo.collider.transform == wallTransform)
                        {
                            // Debug log the hit information
                            Debug.Log($"Ray from {origin} in direction {rayDirection} hit the wall at {hitInfo.point}");
                            Debug.DrawRay(origin, rayDirection * hitInfo.distance, Color.red);

                            // Calculate the new position to avoid the wall
                            Vector3 newPosition = transform.position + rayDirection * (hitInfo.distance - clearance);

                            // Move the player to the new position
                            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
                            isCollisionDetected = true;

                        }
                }

                else
                {
                    // Debug draw for no collision detected
                    Debug.DrawRay(origin, rayDirection * rayLength, Color.green);
                    
                }
            }
        }

        if (isCollisionDetected)
        {
            Debug.Log("The player cannot pass through the wall");
        }

        // If no collision is detected, do something to indicate the clear state
        if (!isCollisionDetected)
        {
            HandleClearState();
        }
    }

    // Get the corner positions of the collider bounds
    private Vector3[] GetBoundsCorners(Bounds bounds)
    {
        Vector3[] corners = new Vector3[8];
        corners[0] = bounds.min;
        corners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        corners[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        corners[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[7] = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        return corners;
    }

    // Handle the clear state when no collision is detected
    private void HandleClearState()
    {
        // Perform actions when the player is clear of the wall
        //Debug.Log("Clear: The player can pass through the wall.");

        // Example: Increase the speed of the wall
        //wallTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Example: Trigger an event or change game state
        // EventManager.TriggerEvent("PlayerPassedThroughWall");
    }

    public void UpdateWallTransform(Transform newWallTransform)
    {
        wallTransform = newWallTransform;
    }
}
