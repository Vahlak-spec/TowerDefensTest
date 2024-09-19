using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.CompilerServices;

public class Projectile : Entity
{
    [SerializeField] private Transform _visual;
    [SerializeField] private float _speed;

    public void Launch(int damage,Vector3 start, Enemy enemy)
    {
        transform.position = start;
        _visual.rotation = Quaternion.FromToRotation(Vector3.up, (enemy.transform.position - start).normalized);
        transform.DOMove(enemy.transform.position, Vector2.Distance(enemy.transform.position, start) / _speed).SetEase(Ease.Linear).
            OnComplete(() => { enemy.TakeDamage(damage); Hide(); });
    }
    public override void Summon()
    {
        base.Summon();
        _visual.gameObject.SetActive(true);
    }
    public override void Hide()
    {
        base.Hide();
        _visual.gameObject.SetActive(false);
        transform.DOKill();
    }

}
