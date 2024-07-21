/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.parent.parent.parent.GetComponent<ParentCollisionDetection>().CollisionDetected(this);
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionDetection : MonoBehaviour
{
    // Define an event that the parent can subscribe to
    public event System.Action CollisionEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision is detected!");
        // Trigger the event when a collision occurs
        CollisionEvent?.Invoke();
    }
}
