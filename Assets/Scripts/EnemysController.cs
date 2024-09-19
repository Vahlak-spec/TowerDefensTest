using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemysController : MonoBehaviour
{

    [SerializeField] private int _enemyNums;
    [SerializeField] private float _enemySpawenDelay;

    private EnemyData[] _enemyDatas;
    private int _tempEnemyId;
    private float _tempSpawenDelay;

    private Action<float> _onEnemyAttack;
    private Action<int> _onCoinTake;
    private Action<int> _onPowerTake;

    public void OnLaunch(Action<float> onEnemyAttack, Action<int> onCoinTake, Action<int> onPowerTake, Level level)
    {
        _onEnemyAttack = onEnemyAttack;
        _onCoinTake = onCoinTake;
        _onPowerTake = onPowerTake;
        _enemyDatas = new EnemyData[_enemyNums];
        _tempEnemyId = 0;
        _tempSpawenDelay = 0;
        for (int i = 0; i < _enemyDatas.Length; i++)
        {
            _enemyDatas[i] = new EnemyData(level.GetEnemy(), level.Curve);
        }
    }
    public void OnFixed()
    {
        _tempSpawenDelay -= Time.fixedDeltaTime;

        if (_tempSpawenDelay <= 0)
        {
            _tempSpawenDelay = _enemySpawenDelay;
            _enemyDatas[_tempEnemyId].Restart(_onEnemyAttack, _onCoinTake, _onPowerTake);
            _tempEnemyId = _tempEnemyId < _enemyDatas.Length - 1 ? _tempEnemyId + 1 : 0;
        }

        foreach (var item in _enemyDatas)
        {
            item.OnFixed();
        }
    }

    [System.Serializable]
    private class EnemySpawnData
    {
        public float spawenChanse;
        public Enemy enemyPref;
    }

    private class EnemyData
    {
        private BezierCurve _bezierCurve;
        private bool _isActive;
        private Entity _enemyPref;
        private Vector3 _offset;
        private float _t;
        private float _tm = 0.99f;
        private float _tc_pf;

        private Enemy _tempEnemy;

        public EnemyData(Entity enemy, BezierCurve bezierCurve)
        {
            _bezierCurve = bezierCurve;
            _enemyPref = enemy;
            _offset = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
            _isActive = false;
        }
        public void OnFixed()
        {
            if (!_isActive) return;

            if (_t >= _tm)
            {
                _tempEnemy.Attack();
            }
            else
            {
                _t += _tc_pf;
                _t = _t > _tm ? _tm : _t;

                _tempEnemy.Mowe(_bezierCurve.GetPointAt(_t) + _offset);
            }
        }
        public void Restart(Action<float> onDamage, Action<int> takeCoins, Action<int> takePower)
        {
            _isActive = true;
            _tempEnemy = Factory.Instanse.Summon<Enemy>(_enemyPref);
            _t = 0;
            _tc_pf = 1f / _tempEnemy.PathTime * Time.fixedDeltaTime;
            _tempEnemy.Lauch(Hide, onDamage, takeCoins, takePower);

        }
        private void Hide()
        {
            if (_tempEnemy != null)
                _tempEnemy.Hide();

            _isActive = false;
        }
    }
}
