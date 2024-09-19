using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : GamePanelBase
{
    public override GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.MainMenu;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _mapButton;
    public override void Launch()
    {
        base.Launch();
        _mapButton.onClick.AddListener(() =>
        {
            _gameController.LaunchPanel(GameSystem.GamePanelType.Map);
        });
    }

    public override void Play()
    {
        base.Play();
    }

    public override void Stop() 
    { 
        base.Stop();
    }
}
