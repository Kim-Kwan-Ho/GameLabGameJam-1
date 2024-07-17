using System.Collections;
using UnityEditor;
using UnityEngine;

public class EpicEnemyMovement : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPosition;
    private ScoreManager scoreManager;
    private Material DamageMaterial;
    private Material EnemyDefaultMaterial;
    private Renderer EnemyRenderer;

    //Enemy Stats
    public float epicEnemyHp;
    public float epicMoveSpeed = 1.0f;
    public float chaseRange = 100f;
    public float hoverHeight = 1.0f;

    //should be replaced
    int bulletDamage = 1;

    [Header("Death Particle")]
    [SerializeField] private GameObject _enemyDeathParticle;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DamageMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/KJK/Material/DamageTakenMat.mat", typeof(Material));
        EnemyDefaultMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/KJK/Material/EnemyDefaultMat.mat", typeof(Material));
        EnemyRenderer = GetComponent<Renderer>();
        scoreManager = GetComponent<ScoreManager>();

        //set enemy hp
        epicEnemyHp = 10;
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
        transform.position += direction * epicMoveSpeed * Time.deltaTime;
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
        epicEnemyHp -= amount;
        Debug.Log($"{gameObject.name} take damaged {amount} & current hp: {epicEnemyHp}");


        if (epicEnemyHp <= 0)
        {
            Debug.Log("killed!");
            scoreManager.IncreaseKillCount();
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