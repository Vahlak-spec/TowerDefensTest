using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendController : MonoBehaviour
{
    private DefendPoint[] _defendPoints;

    private DefendPoint _tempDefPoint;

    private Coroutine _procces;
    private Action _onPointClick;

    public void OnLaunch(Level level, Action onPointClick)
    {
        _onPointClick = onPointClick;
        _defendPoints = level.DefendPoints;
        foreach (var item in _defendPoints)
        {
            item.Init(OnDefPointClick);
            item.Clear();
        }
        _procces = StartCoroutine(Procces());
    }
    private void OnDefPointClick(DefendPoint defendPoint)
    {
        if (_tempDefPoint != null) return;

        _tempDefPoint = defendPoint;
        _onPointClick.Invoke();
    }
    public void SetDefender(Entity defender)
    {
        if (_tempDefPoint == null) return;

        _tempDefPoint.SetDefender(defender);
        ClearTemp();
    }
    public void ClearTemp()
    {
        _tempDefPoint = null;
    }
    public void OnPlay()
    {
        _procces = StartCoroutine(Procces());
    }
    public void OnStop()
    {
        if (_procces != null)
            StopCoroutine(_procces);
    }

    private IEnumerator Procces()
    {
        while (true)
        {
            foreach (var item in _defendPoints)
            {
                item.OnFixed();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
