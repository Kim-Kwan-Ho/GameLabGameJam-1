using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    private int _damage;
    private Vector3 _direction;
    public void Shoot(Vector3 dir, int damage)
    {
        _direction = dir;
        _damage = damage;
    }

    private void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {

        // Enemy Take Damage
        Destroy(this.gameObject);
        Debug.Log(other.gameObject.name + " - " + _damage);
    }
}
