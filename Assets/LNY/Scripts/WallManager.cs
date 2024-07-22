using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wall;  // Reference to the wall GameObject (the entire 5x5 wall grid)
    public int totalSolutionTriggers = 0;  // Total number of SolutionTriggers
    public int activatedSolutionTriggers = 0;  // Number of activated SolutionTriggers
    public bool normalTriggerActivated = false;
    public float clearThreshold = 0.5f;  // Time in seconds to wait for all triggers to be activated
    public LayerMask obstacleLayer;  // LayerMask for the layers considered as obstacles (for checking non-solution BoxColliders)
    public GameObject player;

    private void Start()
    {
        // Find all SolutionTriggers under the wall
        SolutionTrigger[] triggers = wall.GetComponentsInChildren<SolutionTrigger>();
        //player = GameObject.FindGameObjectWithTag("Player");
        totalSolutionTriggers = triggers.Length;
        /*
        Debug.Log("total" + totalSolutionTriggers);
        if(totalSolutionTriggers >= 4)
        {
            foreach (var trigger in  triggers) {
                Debug.Log(trigger);
            }
        }*/
        normalTriggerActivated = false;

    }

    public void OnSolutionTriggerActivated()
    {
        // Increment the count of activated triggers
        activatedSolutionTriggers++;
        Debug.Log(activatedSolutionTriggers);


        // Check if all SolutionTriggers are activated and no non-solution BoxColliders are active
        if (activatedSolutionTriggers == totalSolutionTriggers && AreAllNonSolutionBoxCollidersInactive())
        {
            ClearFunction();
            //StartCoroutine(HandleClearCondition());
        }

        ReturnToPos();

    }

    public void ReturnToPos()
    {
        player.transform.position = new Vector3(0.585638642f, 10.0761595f, -9.35221291f);
    }

    public void OnNormalTriggerActivated()
    {
        normalTriggerActivated = true;
        //Debug.Log("wrongly Triggered");

        ReturnToPos();
    }


    public void OnSolutionTriggerDeactivated()
    {
        // Decrement the count of activated triggers
        activatedSolutionTriggers--;

        // If deactivated, reset the coroutine to ensure that all triggers need to be reactivated
        StopAllCoroutines();
    }

    /*
    private IEnumerator HandleClearCondition()
    {

        yield return new WaitForSeconds(clearThreshold);

        // Check again to confirm all SolutionTriggers are still activated and no non-solution BoxColliders are active
        if (activatedSolutionTriggers == totalSolutionTriggers && AreAllNonSolutionBoxCollidersInactive())
        {
            Debug.Log("entered3");
            ClearFunction(); 
        }
    }*/

    private bool AreAllNonSolutionBoxCollidersInactive()
    {
        if (normalTriggerActivated)
        {
            return false;
        }

        // If no non-SolutionTrigger BoxCollider is a trigger, return true
        return true;
    }

    private void ClearFunction()
    {
        // Define what happens when the clear condition is met
        Debug.Log("All SolutionTriggers activated and no non-solution BoxColliders are active! Clearing level...");

        // Example action on clear
        // Destroy(wall); // Or perform any other level-clearing actions
    }
}