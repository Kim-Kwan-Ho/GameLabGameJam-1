using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private PlayerHealthSystem _healthSystem;
    private int _playerHealth = 3;
    [SerializeField] private GameObject[] _hpGobs;
    [SerializeField] private GameObject _rawImage;
    [SerializeField] ParticleSystem _particle;


    private void Start()
    {
        _healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        _healthSystem.HealthDecreaseEvent += PlayerHealthDecrease;
        _healthSystem.HealthIncreaseEvent += PlayerHealthIncrease;
    }

    private void OnDestroy()
    {
        _healthSystem.HealthDecreaseEvent -= PlayerHealthDecrease;
        _healthSystem.HealthIncreaseEvent -= PlayerHealthIncrease;
    }

    private void PlayerHealthIncrease()
    {
        if (_playerHealth <= Constants.PLAYER_MAXHP)
        {
            _playerHealth++;
            _hpGobs[_playerHealth - 1].SetActive(true);
            _rawImage.transform.position = _hpGobs[_playerHealth - 1].transform.position;
        }
    }

    private void PlayerHealthDecrease()
    {
        if (_playerHealth > 0)
        {
            _rawImage.transform.position = _hpGobs[_playerHealth - 1].transform.position;
            _hpGobs[_playerHealth - 1].SetActive(false);
            _playerHealth--;
            _particle.Play();
        }
    }
}
