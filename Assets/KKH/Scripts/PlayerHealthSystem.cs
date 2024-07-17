using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public int Health{get {return _health;}}
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
            _health++;
        }
    }
}
