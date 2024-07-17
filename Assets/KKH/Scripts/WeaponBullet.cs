using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{

    [SerializeField] private GameObject[] _bulletGobs;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    private int _damage;
    private Vector3 _direction;
    public void Shoot(Vector3 dir, int damage, int level)
    {
        _direction = dir;
        _damage = damage;
        _bulletGobs[level - 1].SetActive(true);
    }

    private void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();
            enemy.TakeDamage(_damage);
        }
        Destroy(this.gameObject);
    }
}
