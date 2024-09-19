using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Image[] _weaponIcons;
    [SerializeField] private Image _relodeFill;
    [SerializeField] WeaponSO[] _weaponSOs;
    [SerializeField] private Entity _damageCuster;

    private int _weaponIndex;
    private float _tempDelay;

    public void OnLaunch()
    {
        int tempWeapon = GameSystem.GetInt(GameSystem.SaveValueType.TempWeapon);
        for (int i = 0; i < _weaponSOs.Length; i++)
        {
            if (_weaponSOs[i].Id == tempWeapon)
            {
                _weaponIndex = i;
                _tempDelay = 0;

                foreach (var item in _weaponIcons)
                    item.sprite = _weaponSOs[i].Icon;

                break;
            }
        }
    }
    public void OnFixed()
    {
        _tempDelay -= Time.fixedDeltaTime;

        _relodeFill.fillAmount = _tempDelay / _weaponSOs[_weaponIndex].AttackDelay;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _tempDelay <= 0)
        {
            Factory.Instanse.Summon<DamageCuster>(_damageCuster).Launch(_weaponSOs[_weaponIndex].Damage, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            _tempDelay = _weaponSOs[_weaponIndex].AttackDelay;
        }
    }
}
