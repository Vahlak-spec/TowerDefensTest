using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class GamePanelBase : MonoBehaviour
{
    public virtual GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.None;

    protected GameController _gameController;

    [SerializeField] private GameObject[] _GO;

    public virtual void Launch() { foreach (var item in _GO) { item.SetActive(true); } }
    public virtual void Play() { foreach (var item in _GO) { item.SetActive(true); } }
    public virtual void Stop() { foreach (var item in _GO) { item.SetActive(false); } }
    public virtual void Init(GameController gameController)
    {
        _gameController = gameController;
        foreach (var item in _GO) { item.SetActive(false); }
    }
}
