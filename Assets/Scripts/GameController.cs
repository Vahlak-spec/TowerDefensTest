using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameSystem.GamePanelType _startPanel;
    [SerializeField] private GamePanelBase[] _gamePanels;

    public void PlayPanel(GameSystem.GamePanelType gamePanelType)
    {
        foreach(var item in _gamePanels)
        {
            if (item.GamePanelType == gamePanelType)
                item.Play();
            else
                item.Stop();
        }
    }
    public void LaunchPanel(GameSystem.GamePanelType gamePanelType)
    {
        foreach (var item in _gamePanels)
        {
            if (item.GamePanelType == gamePanelType)
                item.Launch();
            else
                item.Stop();
        }
    }
    public void Start()
    {
        foreach(var item in _gamePanels)
        {
            item.Init(this);
            if (item.GamePanelType == _startPanel)
                item.Launch();
            else
                item.Stop();
        }
    }
}
