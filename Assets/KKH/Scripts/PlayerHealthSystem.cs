using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    [SerializeField] private GameObject _deathParticle;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle")) 
        {
            //Destroy(this.gameObject); // 게임종료
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
