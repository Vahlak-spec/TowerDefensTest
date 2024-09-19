using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : GamePanelBase
{
    public override GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.Pause;

    [SerializeField] private Button[] _playButtons;

    public override void Launch()
    {
        base.Launch();
        foreach (var button in _playButtons) 
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { _gameController.PlayPanel(GameSystem.GamePanelType.Game); });
        }
    }
}
