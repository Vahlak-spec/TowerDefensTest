using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BustDefend", menuName = "ScriptableObjects/BustDefend")]
public class BustDefend : BustSO
{
    [SerializeField] private float _newDamageModiff;
    public override void Activate(BustExecutor executor)
    {
        executor.DamageModif = _newDamageModiff;
    }

    public override void DeActivate(BustExecutor executor)
    {
        executor.DamageModif = 1;
    }
}
