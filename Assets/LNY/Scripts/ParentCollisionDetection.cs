using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParentCollisionDetection : MonoBehaviour
{
    public int answerCount = 4; // The number of collisions needed
    private int collisionCount = 0; // Counter for detected collisions

    private void OnEnable()
    {
        // Subscribe to the collision event for child components
        foreach (var child in GetComponentsInChildren<ChildCollisionDetection>())
        {
            child.CollisionEvent += CollisionDetected;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the collision event to avoid memory leaks
        foreach (var child in GetComponentsInChildren<ChildCollisionDetection>())
        {
            child.CollisionEvent -= CollisionDetected;
        }
    }

    // This method will be called when a child's collision event is triggered
    public void CollisionDetected()
    {
        collisionCount++;
        Debug.Log("Child collided. Total collisions: " + collisionCount);

        if (collisionCount >= answerCount)
        {
            // Perform the action when the exact number of collisions is met
            Debug.Log("Collision count has reached the answer count.");
            // Call the method for the action you want to perform
            StageClear();
        }
    }

    private void StageClear()
    {
        Debug.Log("Doing something as collision count met the target.");
        Destroy(gameObject);
    }
}