using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float PathTime => _pathTime;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _power;
    [SerializeField] private int _coins;
    [SerializeField] private float _atackCoolDown;
    [SerializeField] private float _pathTime;
    [Space]
    [SerializeField] private Transform _visual;
    [SerializeField] private Collider2D _collider;

    private int _tempHealth;
    private float _tempAttackCooldown;

    private Action _onDeath;
    private Action<float> _damageCust;
    private Action<int> _takeCoins;
    private Action<int> _takePower;

    public void Lauch(Action onDeath, Action<float> damageCust, Action<int> takeCoins, Action<int> takePower)
    {
        _onDeath = onDeath;
        _takeCoins = takeCoins;
        _takePower = takePower;
        _damageCust = damageCust;
        _tempAttackCooldown = 0;
        _tempHealth = _health;
        _collider.enabled = true;
        _visual.gameObject.SetActive(true);
    }
    public override void Hide()
    {
        base.Hide();
        _collider.enabled = false;
        _visual.gameObject.SetActive(false);
    }

    public void Mowe(Vector3 position)
    {
        transform.position = position;
    }
    public void Attack()
    {
        _tempAttackCooldown -= Time.fixedDeltaTime;

        if(_tempAttackCooldown <= 0)
        {
            _tempAttackCooldown = _atackCoolDown;
            _damageCust?.Invoke(_damage);
        }
    }
    public void TakeDamage(int value)
    {
        Debug.Log("TakeDamage");
        _tempHealth -= value;

        if(_tempHealth <= 0)
        {
            _onDeath?.Invoke();
            _takeCoins?.Invoke(_coins);
            _takePower?.Invoke(_power);
        }
    }
}
