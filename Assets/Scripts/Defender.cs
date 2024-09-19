using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Entity
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Transform _projectileStart;
    [Space]
    [SerializeField] private float _attackDelay;
    [SerializeField] private int _damage;
    [SerializeField] private Entity _projevtilePrefab;

    private float _tempAttackDelay;

    private List<Enemy> _enemys = new List<Enemy>();

    public override void Summon()
    {
        _sprite.enabled = true;
        _collider.enabled = true;
        _tempAttackDelay = _attackDelay;
        base.Summon();
    }
    public override void Hide()
    {
        _sprite.enabled = false;
        _collider.enabled = false;
        _enemys.Clear();
        base.Hide();
    }
    public void OnFixed()
    {
        _tempAttackDelay -= Time.fixedDeltaTime;
        if(_tempAttackDelay <= 0 && _enemys.Count > 0)
        {
            _tempAttackDelay = _attackDelay;
            Factory.Instanse.Summon<Projectile>(_projevtilePrefab).Launch(_damage, _projectileStart.position, _enemys[0]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy) && !_enemys.Contains(enemy))
        {
            _enemys.Add(enemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && _enemys.Contains(enemy))
        {
            _enemys.Remove(enemy);
        }
    }
}
