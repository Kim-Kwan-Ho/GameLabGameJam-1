using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

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
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DamageMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Material/DamageTakenMat.mat", typeof(Material));
        EnemyDefaultMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Material/EnemyDefaultMat.mat", typeof(Material));
        scoreManager = new ScoreManager();
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

        if (other.tag == "Bullet")
        {
            Debug.Log("collided with bullet");
            TakeDamage(bulletDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        Flash();
        hp -= amount;

        if (hp <= 0)
        {
            Debug.Log("killed!");
            scoreManager.IncreaseKillCount();
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