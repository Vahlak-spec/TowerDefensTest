using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory Instanse => instance;

    private static Factory instance;

    [SerializeField] private FactoryData[] _datas;

    private void Start()
    {
        if (instance != null)
            return;
            
        instance = this;

        foreach(var item in _datas)
        {
            item.Init();
        }
    }
    public T Summon<T>(Entity entity)
    {
        foreach(var item in _datas)
        {
            if (item.CanSummon(entity))
            {
                return item.Summon<T>();
            }
        }
        return default(T);
    }

    [System.Serializable]
    private class FactoryData
    {
        [SerializeField] private Entity _prefab;
        [SerializeField] private int _num;

        private Entity[] _entities; 

        public void Init()
        {
            _entities = new Entity[_num];

            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i] = Instantiate(_prefab);
                _entities[i].Hide();
            }
        }
        public bool CanSummon(Entity entity) => entity == _prefab;

        public T Summon<T>()
        {
            foreach(var item in _entities)
            {
                if (item.IsHide)
                {
                    item.Summon();
                    return item.GetComponent<T>();
                }
            }
            return default(T);
        }  
    }
}
