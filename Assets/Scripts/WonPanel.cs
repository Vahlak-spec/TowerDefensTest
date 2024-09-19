using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WonPanel : GamePanelBase
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _closeButton;
    public override GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.Won;

    public override void Launch()
    {
        base.Launch();
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() => { _gameController.LaunchPanel(GameSystem.GamePanelType.MainMenu); });
        _text.text = "Level " + (GameSystem.GetInt(GameSystem.SaveValueType.TempLevel) + 1).ToString() + "\r\nVictory";
    }
}
