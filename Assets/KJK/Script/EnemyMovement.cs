using System.Collections;
using UnityEditor;
using UnityEngine;

public class FlyingEnemyChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 100f;
    public float moveSpeed = 3.0f;
    public float hoverHeight = 1.0f;
    private Vector3 targetPosition;
    private ScoreManager scoreManager;
    private Material DamageMaterial;
    private Material EnemyDefaultMaterial;
    private Renderer EnemyRenderer;
    public float hp;
    int bulletDamage = 1;

    [Header("Death Particle")]
    [SerializeField] private GameObject _enemyDeathParticle;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DamageMaterial = Resources.Load<Material>("Materials/DamageTakenMat");
        EnemyDefaultMaterial = Resources.Load<Material>("Materials/EnemyDefaultMat");
        EnemyRenderer = GetComponent<Renderer>();
        Debug.Log("Material loaded");
        hp = 3;
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
        }
    }

    public void TakeDamage(int amount)
    {
        Flash();
        hp -= amount;
        Debug.Log($"{gameObject.name} take damaged {amount} & current hp: {hp}");

        if (hp <= 0)
        {
            Debug.Log("killed!");
            //scoreManager.IncreaseKillCount();
            Instantiate(_enemyDeathParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        EnemyRenderer.material = DamageMaterial;

        yield return new WaitForSeconds(0.1f);

        EnemyRenderer.material = EnemyDefaultMaterial;
    }
}