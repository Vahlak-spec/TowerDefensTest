using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public int Id => _id;
    public int Damage => _damage;
    public Sprite Icon => _icon;
    public float Range => _range;
    public float AttackDelay => _attackDelay;

    [SerializeField] private int _id;
    [SerializeField] private int _damage;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _range;
    [SerializeField] private float _attackDelay;
}
