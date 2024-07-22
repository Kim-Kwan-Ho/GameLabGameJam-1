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

    public bool canTakeDamage = true;
    public float hitCooldown = 3f;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle"))
        {
            // Get the parent¡¯s parent¡¯s parent GameObject
            GameObject entireGameObject = col.transform.parent?.parent?.parent?.gameObject;

            Destroy(entireGameObject);
            PlayerTakeHit();

        }
    }


    private void PlayerTakeHit()
    {
        //StartCoroutine(PlayerHitEffect());
        
        if (canTakeDamage)
        {
            _health--;
            Debug.Log("Health" + _health);

            if (_health > 0)
            {
                StartCoroutine(HitCooldownCoroutine());
            }

            HealthDecreaseEvent?.Invoke();

        }

        if (_health <= 0)
        {
            PlayerDeath();
        }

    }

    private IEnumerator HitCooldownCoroutine()
    {
        // Set canTakeDamage to false to prevent further damage
        canTakeDamage = false;

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(hitCooldown);

        // Re-enable damage
        canTakeDamage = true;
    }

    /*
    private IEnumerator PlayerHitEffect()
    {
        _meshRenderer.material = _materials[1];
        yield return new WaitForSeconds(_hitEffectTime);
        _meshRenderer.material = _materials[0];
    }*/

    private void PlayerDeath()
    {
        //_meshRenderer.enabled = false;
        //GetComponent<Collider>().enabled = false;
        //GetComponent<PlayerAttack>().enabled = false;
        //GetComponent<PlayerMoving>().enabled = false;
        _deathParticle.SetActive(true);

        GameSceneManager.Instance.GameSceneEvent.CallGameOver();
    }

    public void RecoverHealth()
    {
        if (_health >= Constants.PLAYER_MAXHP)
        {
            ScoreManager.instance.IncreaseItemScore(Constants.SCORE_HPITEM);
        }
        else
        {
            HealthIncreaseEvent?.Invoke();
            _health++;
        }
    }




}
