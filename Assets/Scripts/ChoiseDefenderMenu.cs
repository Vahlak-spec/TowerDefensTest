using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiseDefenderMenu : MonoBehaviour
{
    [SerializeField] private GameObject _GO;
    [SerializeField] private Button _closeButton;
    [SerializeField] ChoiseItem[] _choiseItems;

    public void OnLaunch(DefenderSO[] defenderSO, Action<int, Entity> onClick, Action onClose)
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(onClose.Invoke);

        foreach(var item in _choiseItems)
        {
            item.Hide();
        }

        for(int i = 0; i < defenderSO.Length; i++)
        {
            _choiseItems[i].Init(defenderSO[i], onClick);
        }
    }
    public void Show() => _GO.SetActive(true);
    public void Hide() => _GO.SetActive(false);

    [System.Serializable]
    private class ChoiseItem
    {
        [SerializeField] private GameObject _go;
        [SerializeField] private Image _logo;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _price;

        public void Init(DefenderSO defenderSO, Action<int, Entity> onClickAction)
        {
            _go.SetActive(true);
            _logo.sprite = defenderSO.Logo;
            _price.text = defenderSO.Price.ToString();

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => 
            {
                onClickAction.Invoke(defenderSO.Price, defenderSO.Pref);
            });
        }
        public void Hide()
        {
            _go.SetActive(false);
        }
    }
}
