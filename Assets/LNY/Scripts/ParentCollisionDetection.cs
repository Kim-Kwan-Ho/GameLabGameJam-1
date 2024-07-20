using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCollisionDetection : MonoBehaviour
{
    public void CollisionDetected(ChildCollisionDetection childCollisionDetection)
    {
        //decrease player hp
        Debug.Log("child Collided");

    }
}
