using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackSpeed = 0.85f;
    private float _attackCoolTime = 0;
    private bool _attackable = true;
    private int _damageLevel = 1;
    private int _attackSpeedLevel = 1;
    
    
    


    [Header("Enemy & Scanner")]
    [SerializeField] private float _searchSize = 5f;
    [SerializeField] private GameObject _enemy; // 이건 enemy 완성 후 설정
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _searchTime = 0.1f;



    private bool _isSearching = true;


    private void Start()
    {
        StartCoroutine(CoSearchEnemy());
    }


    private void Update()
    {
        if (!_attackable || _attackCoolTime > 0)
        {
            _attackCoolTime -= Time.deltaTime;

            if (_attackCoolTime < 0)
            {
                _attackCoolTime = 0;
                _attackable = true;
            }
        }


        if (_enemy == null && !_isSearching)
        {
            _isSearching = true;
            StartCoroutine(CoSearchEnemy());
        }
        else if (_enemy != null && _attackable)
        {
            FireWeapon();
        }
    }


    private IEnumerator CoSearchEnemy()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, _searchSize, _enemyLayer);
        if (col.Length >= 1 && col[0] != null)
        {
            _isSearching = false;
            _enemy = col[0].transform.gameObject;
        }
        else
        {
            yield return new WaitForSeconds(_searchTime);
            StartCoroutine(CoSearchEnemy());
        }
    }

    private void FireWeapon()
    {
        _attackable = false;
        _attackCoolTime = _attackSpeed;
        WeaponBullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).GetComponent<WeaponBullet>();
        bullet.Shoot(_enemy.transform.position - transform.position, _damage);

    }

    private void DamageLevelUp()
    {
        if (_damageLevel >= Constants.PLAYER_MAXLEVEL)
        {
            // 점수 추가
        }
        else
        {
            _damageLevel++;
            _damage = _damageLevel;
        }

    }

    private void AttackSpeedLevelUp()
    {
        if (_attackSpeedLevel >= Constants.PLAYER_MAXLEVEL)
        {
            // 점수 추가
        }
        else
        {
            _attackSpeedLevel++;
            _attackSpeed = 1 - (_attackSpeedLevel * 0.15f);
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchSize);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Items item = other.gameObject.GetComponent<Items>();
            switch (item.ItemType)
            {
                case EItemType.AttackSpeed:
                    AttackSpeedLevelUp();
                    break;
                case EItemType.Damage:
                    DamageLevelUp();
                    break;
                case EItemType.Score: // 스코어 추가
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}



