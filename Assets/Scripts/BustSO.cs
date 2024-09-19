using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BustSO : ScriptableObject
{
    public float Duration => _duration;
    public float CoolDown => _coolDown;
    public int Price => _price;

    [SerializeField] private float _duration;
    [SerializeField] private float _coolDown;
    [SerializeField] private int _price;

    public abstract void Activate(BustExecutor executor);
    public abstract void DeActivate(BustExecutor executor);
}
