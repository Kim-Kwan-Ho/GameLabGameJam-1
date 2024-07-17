using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public int Health { get { return _health; } }
    [SerializeField] private int _health = 3;

    [SerializeField] private GameObject _deathParticle;
    [SerializeField] private Material[] _materials = new Material[2];
    [SerializeField] private float _hitEffectTime = 0.2f;
    public Action HealthDecreaseEvent;
    public Action HealthIncreaseEvent;


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
        StartCoroutine(PlayerHitEffect());
        _health--;
        HealthDecreaseEvent?.Invoke();
        if (_health <= 0)
        {
            PlayerDeath();
        }
    }

    private IEnumerator PlayerHitEffect()
    {
        _meshRenderer.material = _materials[1];
        yield return new WaitForSeconds(_hitEffectTime);
        _meshRenderer.material = _materials[0];
    }

    private void PlayerDeath()
    {
        _meshRenderer.enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerMoving>().enabled = false;
        _deathParticle.SetActive(true);

        GameSceneManager.Instance.GameSceneEvent.CallGameOver();
    }

    public void RecoverHealth()
    {
        if (_health >= Constants.PLAYER_MAXHP)
        {
            // 점수 추가
        }
        else
        {
            HealthIncreaseEvent?.Invoke();
            _health++;
        }
    }




}
