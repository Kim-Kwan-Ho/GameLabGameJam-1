using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private int _health = 3;

    [SerializeField] private GameObject _deathParticle;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle")) 
        {
            Destroy(col.gameObject); // 게임종료
            PlayerTakeHit();
        }
    }


    private void PlayerTakeHit()
    {
        _health--;
        Debug.Log("Health: " + _health);
        if (_health <= 0)
        {
            PlayerDeath();
        }
   
    }

    private void PlayerDeath()
    {
        _meshRenderer.enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerMoving>().enabled = false;
        _deathParticle.SetActive(true);
    }
}
