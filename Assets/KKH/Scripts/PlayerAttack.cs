using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackSpeed = 0.85f;
    private float _attackCoolTime = 0;
    private bool _attackable = true;
    public int DamageLevel { get { return _damageLevel; } }
    private int _damageLevel = 1;
    public int AttackSpeedLevel { get { return _attackSpeedLevel; } }
    private int _attackSpeedLevel = 1;





    [Header("Enemy & Scanner")]
    [SerializeField] private float _searchSize = 5f;
    [SerializeField] private GameObject _enemy; // 이건 enemy 완성 후 설정
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
        Collider[] col = Physics.OverlapSphere(transform.position, _searchSize, 1 << 10);
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
        bullet.Shoot(_enemy.transform.position - transform.position, _damage, _damageLevel);

    }

    private void DamageLevelUp()
    {
        if (_damageLevel >= Constants.PLAYER_MAXLEVEL)
        {
            ScoreManager.instance.IncreaseItemScore(Constants.SCORE_UPGRADEITEM);
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
            ScoreManager.instance.IncreaseItemScore(Constants.SCORE_UPGRADEITEM);
        }
        else
        {
            _attackSpeedLevel++;
            _attackSpeed = 1 - (_attackSpeedLevel * 0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Items item = other.gameObject.GetComponent<Items>();
            item.InstantiateParticle();
            switch (item.ItemType)
            {
                case EItemType.AttackSpeed:
                    AttackSpeedLevelUp();
                    break;
                case EItemType.Damage:
                    DamageLevelUp();
                    break;
                case EItemType.Score:
                    ScoreManager.instance.IncreaseItemScore(Constants.SCORE_SCOREITEM);
                    break;
                case EItemType.Health:
                    GetComponent<PlayerHealthSystem>().RecoverHealth();
                    GameSceneManager.Instance.GameSceneEvent.CallOnGameResume();
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}



