using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : GamePanelBase, BustExecutor
{
    public override GameSystem.GamePanelType GamePanelType => GameSystem.GamePanelType.Game;

    [SerializeField] private Image _weaponIcon;
    [SerializeField] private BustData[] _bustDatas;
    [Space]
    [SerializeField] private TextMeshProUGUI _powerText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Button _pauseButton;
    [Space]
    [SerializeField] private ChoiseDefenderMenu _chooseDefenderMenu;
    [SerializeField] private DefendController _defendController;
    [SerializeField] private WeaponController _weaponController;
    [Space]
    [SerializeField] private Image _playerHPBar;
    [Space]
    [SerializeField] private EnemysController _eneemysController;
    [SerializeField] private Level[] _levels;
    [Space]
    [SerializeField] private int _MaxHP;
    [SerializeField] private DefenderSO[] _defenderSOs;

    private Coroutine _procces;
    private int _power;
    private int _coins;
    private float _tempHP;
    private float _time;
    private Level _tempLevel;

    public float DamageModif { set { _damageGetModif = value; } }

    private float _damageGetModif = 1;

    public int Power
    {
        get { return _power; }
        set
        {
            _powerText.text = value.ToString();
            _power = value;
        }
    }
    public float TimeLeft
    {
        get { return _time; }
        set
        {
            _time = value;
            _timeText.text = Mathf.FloorToInt(_time / 60).ToString() + ":" + Mathf.FloorToInt(_time % 60);
        }
    }
    public int Coins
    {
        get { return _coins; }
        set
        {
            _coinText.text = value.ToString();
            _coins = value;
        }
    }
    public float HP
    {
        get { return _tempHP; }
        set
        {
            _tempHP = value;
            _playerHPBar.fillAmount = (float)_tempHP / (float)_MaxHP;
        }
    }

    public bool TryCust(int price)
    {
        if (Power >= price)
        {
            Power -= price;
            return true;
        }
        return false;
    }
    public override void Launch()
    {
        base.Launch();
        HP = _MaxHP;
        Power = 10;
        Coins = 0;
        foreach (var item in _bustDatas)
        {
            item.OnLaunch();
        }

        int li = GameSystem.GetInt(GameSystem.SaveValueType.TempLevel);
        foreach(var item in _levels)
        {
            item.gameObject.SetActive(false);
            if(li == item.Id)
                _tempLevel = item;
        }
        _tempLevel.gameObject.SetActive(true);

        TimeLeft = _tempLevel.LevelTime();
        _eneemysController.OnLaunch(TakeDamage, TakeCoin, TakePower, _tempLevel);
        _chooseDefenderMenu.OnLaunch(_defenderSOs, TrysummonDefender, CloseChoiseDefMenu);
        _defendController.OnLaunch(_tempLevel, SummonChoiseDefendMenu);
        _weaponController.OnLaunch();

        _chooseDefenderMenu.Hide();
        _tempLevel.OnLauch();
        _procces = StartCoroutine(Procces());
    }
    public override void Play()
    {
        base.Play();
        _procces = StartCoroutine(Procces());
        _defendController.OnPlay();
    }
    public override void Stop()
    {
        base.Stop();
        if (_procces != null)
        {
            StopCoroutine(_procces);
        }
    }

    public override void Init(GameController gameController)
    {
        base.Init(gameController);
        foreach (var item in _bustDatas)
        {
            item.Init(this);
        }
        _pauseButton.onClick.AddListener(() => { _gameController.LaunchPanel(GameSystem.GamePanelType.Pause); });
    }
    private void SummonChoiseDefendMenu()
    {
        _chooseDefenderMenu.Show();
    }
    private void TrysummonDefender(int price, Entity pref)
    {
        if (Power < price) return;

        Power -= price;
        _chooseDefenderMenu.Hide();
        _defendController.SetDefender(pref);
    }
    private void CloseChoiseDefMenu()
    {
        _chooseDefenderMenu.Hide();
        _defendController.ClearTemp();
    }
    private void TakeDamage(float damage)
    {
        HP -= (damage * _damageGetModif);
        
        if(HP < 0)
        {
            Lose();
        }
    }
    private void TakeCoin(int coin) => Coins += coin;
    private void TakePower(int power) => Power += power;

    private void Lose()
    {
        _gameController.LaunchPanel(GameSystem.GamePanelType.Lose);
    }
    private void Won()
    {
        _gameController.LaunchPanel(GameSystem.GamePanelType.Won);
    }

    private IEnumerator Procces()
    {
        while (true)
        {
            foreach (var item in _bustDatas)
            {
                item.OnFixed();
            }
            _tempLevel.OnFixed();
            _weaponController.OnFixed();
            _eneemysController.OnFixed();
            TimeLeft -= Time.fixedDeltaTime;

            if (TimeLeft < 0) Won();

            yield return new WaitForFixedUpdate();
        }
    }

    [System.Serializable]
    private class BustData
    {
        [SerializeField] private BustSO _bustSO;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fillIcon;
        [SerializeField] private Button _button;

        private BustExecutor _executor;
        private BustState _bustState;
        private float _timeLeft;

        public void Init(BustExecutor executor)
        {
            _executor = executor;
            _button.onClick.AddListener(OnClick);
        }
        public void OnLaunch()
        {
            _fillIcon.fillAmount = 0;
            _bustState = BustState.ReadyToUse;
            _icon.color = Color.gray;
        }

        private void OnClick()
        {
            if (_bustState != BustState.ReadyToUse) return;
            if (!_executor.TryCust(_bustSO.Price)) return;

            _bustState = BustState.Active;
            _timeLeft = _bustSO.Duration + _bustSO.CoolDown;
            _bustSO.Activate(_executor);
        }

        public void OnFixed()
        {
            if (_bustState == BustState.ReadyToUse) return;

            _timeLeft -= Time.fixedDeltaTime;

            if (_timeLeft > 0) return;

            switch (_bustState)
            {
                case BustState.Active:
                    _timeLeft = _bustSO.CoolDown;
                    _bustState = BustState.CoolDown;
                    break;
                case BustState.CoolDown:
                    _bustState = BustState.ReadyToUse;
                    break;
            }
        }

        private enum BustState
        {
            ReadyToUse,
            Active,
            CoolDown
        }
    }
}
