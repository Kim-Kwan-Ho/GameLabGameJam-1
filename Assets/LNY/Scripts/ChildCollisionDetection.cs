using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.parent.parent.GetComponent<ParentCollisionDetection>().CollisionDetected(this);
    }
}
