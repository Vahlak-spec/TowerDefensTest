using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BustExecutor
{
    public bool TryCust(int price);
    public float DamageModif { set; }
}
