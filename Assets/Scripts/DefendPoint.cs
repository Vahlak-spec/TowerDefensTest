using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefendPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _nonesprite;
    [SerializeField] private Collider2D _collider;

    private Action<DefendPoint> _onMouseDown;
    private Defender _defender;

    public void Init(Action<DefendPoint> onMouseDown)
    {
        _onMouseDown = onMouseDown;
    }
    public void SetDefender(Entity defenderPrefab)
    {
        _defender = Factory.Instanse.Summon<Defender>(defenderPrefab);
        _defender.transform.position = transform.position;
        _nonesprite.enabled = false;
        _collider.enabled = false;
    }
    public void Clear()
    {
        if (_defender != null)
            _defender.Hide();

        _defender = null;
        _nonesprite.enabled = true;
        _collider.enabled = true;
    }
    public void OnFixed()
    {
        if (_defender == null)
            return;

        _defender.OnFixed();
    }
    private void OnMouseDown()
    {
        _onMouseDown.Invoke(this);
    }
}
