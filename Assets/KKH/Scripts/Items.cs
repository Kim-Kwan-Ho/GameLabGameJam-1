using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public EItemType ItemType;

    [SerializeField] private float _lifeTime = 7f;
    [SerializeField] private float _rotSpeed = 10f;
    [SerializeField] private GameObject _particleGob;

    void Start()
    {
        for (int i = 0; i < LevelManager.Instance.EatenHeart.Count; i++)
        {
            if (LevelManager.Instance.EatenHeart[i] == LevelManager.Instance.CurrentCoordinate)
                Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed, 0) * Time.deltaTime);
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetItem(float lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void InstantiateParticle()
    {
        Instantiate(_particleGob, transform.position, transform.rotation);
    }
}


public enum EItemType
{
    Damage,
    AttackSpeed,
    Score,
    Health
}








