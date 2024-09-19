using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : GamePanelBase
{
    public override GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.Map;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private ChoiseLevelItem[] _choiseLevelItems;

    private int _tempLevel;

    public override void Init(GameController gameController)
    {
        base.Init(gameController);
        foreach (var item in _choiseLevelItems)
        {
            item.SetOnClick(ClickLevelButton);
        }

        _playButton.onClick.AddListener(ClickPlayButton);
        _exitButton.onClick.AddListener(ClickExitButton);
    }

    public override void Launch()
    {
        base.Launch();
        OnPlay();
    }
    public override void Play()
    {
        base.Play();
        OnPlay();
    }
    public override void Stop()
    {
        base.Stop();
        GameSystem.SetInt(GameSystem.SaveValueType.TempLevel, _tempLevel);
    }

    private void OnPlay() 
    {
        ClickLevelButton(GameSystem.GetInt(GameSystem.SaveValueType.TempLevel));
    }

    private void ClickPlayButton()
    {
        GameSystem.SetInt(GameSystem.SaveValueType.TempLevel, _tempLevel);
        _gameController.LaunchPanel(GameSystem.GamePanelType.Game);
    }

    private void ClickExitButton()
    {
        GameSystem.SetInt(GameSystem.SaveValueType.TempLevel, _tempLevel);
        _gameController.LaunchPanel(GameSystem.GamePanelType.MainMenu);
    }

    private void ClickLevelButton(int levelNum)
    {
        _tempLevel = levelNum;
        foreach(var item in _choiseLevelItems)
        {
            item.TryActivate(levelNum);
        }
    }

    

    [System.Serializable]
    private class ChoiseLevelItem
    {
        [SerializeField] private int _levelNum;
        [SerializeField] private Button _button;

        private Action<int> _clickAction;

        public void SetOnClick(Action<int> onClick)
        {
            _clickAction = onClick;

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() 
        {
            _clickAction.Invoke(_levelNum);
        }

        public void TryActivate(int num)
        {
            _button.transform.DOKill();

            if (num == _levelNum)
            {
                _button.transform.DOScale(1.25f, 0.5f);
            }
            else
            {
                _button.transform.DOScale(1f, 0.5f);
            }
        }
    }

}
