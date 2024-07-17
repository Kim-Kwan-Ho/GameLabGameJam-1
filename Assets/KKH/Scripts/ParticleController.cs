using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1.0f;

    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0.0f)
            Destroy(this.gameObject);
    }
}
