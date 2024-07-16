using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class FlyingEnemyChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 100f;
    public float moveSpeed = 3.0f;
    public float hoverHeight = 1.0f;
    private Vector3 targetPosition;

    public float hp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (player == null) return;

        if (IsPlayerInRange())
        {
            // Calculate target position above or below the player
            targetPosition = player.position + Vector3.up * hoverHeight;
            MoveTowardsTarget();
        }


    }

    bool IsPlayerInRange()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        return Mathf.Abs(distanceToPlayer.x) <= chaseRange && Mathf.Abs(distanceToPlayer.y) <= chaseRange && Mathf.Abs(distanceToPlayer.z) <= chaseRange;
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            //kill Player
        }
    }

    void EnemySpawn()
    {

    }
}