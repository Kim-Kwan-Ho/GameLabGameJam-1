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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Obstacle")) ;
        {
            //Destroy(this.gameObject); // 게임종료
            _meshRenderer.enabled = false;
            _deathParticle.SetActive(true);


        }
    }
}
