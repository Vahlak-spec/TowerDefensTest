using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCuster : Entity
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _lifeTime;

    private int _damage;

    public override void Hide()
    {
        base.Hide();
        _collider.enabled = false;
        transform.DOKill();
    }
    public void Launch(int damage,Vector3 position)
    {
        Debug.Log("Launch");
        transform.position = position;
        _damage = damage;
        _collider.enabled = true;

        DOVirtual.DelayedCall(1, Hide);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DamageCusterTrigger");
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Hide();
        }
    }
}
