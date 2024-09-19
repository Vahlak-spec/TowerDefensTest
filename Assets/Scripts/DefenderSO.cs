using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenderSO", menuName = "ScriptableObjects/DefenderSO")]
public class DefenderSO : ScriptableObject
{
    public Sprite Logo => _logo;
    public int Price => _price;
    public Entity Pref => _pref;

    [SerializeField] private Sprite _logo;
    [SerializeField] private int _price;
    [SerializeField] private Entity _pref;
}
