using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int Id => _levelId;
    public DefendPoint[] DefendPoints => _defendPoints;

    [SerializeField] private int _levelId;
    [SerializeField] private BezierCurve _path;
    [SerializeField] private EnemySpawnData[] _enemySpawnData;
    [SerializeField] private LevelEtape[] _levelEtapes;
    [SerializeField] private DefendPoint[] _defendPoints;

    public BezierCurve Curve => _path;
    public Enemy GetEnemy() 
    {
        Debug.Log("ddddddd");

        float r = Random.Range(0, 100);
        float d = 0;

        foreach(var item in _enemySpawnData)
        {
            d += item.spawenChanse;
            if (d > r)
            {
                Debug.Log("dfdfsdfadd");
                return item.enemyPref;
            }
        }

        return _enemySpawnData[0].enemyPref;
    }

    public float GetSpawenDelay => _levelEtapes[_tempEtapId].GetSpawenDelay;

    private int _tempEtapId;
    private float _toNextEtap;

    public float LevelTime()
    {
        float res = 0;
        foreach(var item in _levelEtapes)
        {
            res += item.Time;
        }
        return res;
    }
    public void OnLauch()
    {
        _tempEtapId = 0;
        _toNextEtap = _levelEtapes[_tempEtapId].Time;
    }

    public void OnFixed()
    {
        _toNextEtap -= Time.fixedDeltaTime;

        if(_toNextEtap <= 0)
        {
            if (_tempEtapId == _levelEtapes.Length - 1)
                _toNextEtap = float.MaxValue;
            else
                _tempEtapId++;
        }
    }

    [System.Serializable]
    private class LevelEtape
    {
        public float Time => _time;
        public float GetSpawenDelay => UnityEngine.Random.Range(_minSpawenDelay, _maxSpawenDelay);

        [SerializeField] private float _time;
        [SerializeField] private float _minSpawenDelay;
        [SerializeField] private float _maxSpawenDelay;
    }

    [System.Serializable]
    private class EnemySpawnData
    {
        public float spawenChanse;
        public Enemy enemyPref;
    }
}
