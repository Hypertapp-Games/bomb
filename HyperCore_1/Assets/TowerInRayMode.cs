using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInRayMode : MonoBehaviour
{
    public Tower thisTurret;

    private void Start()
    {
        thisTurret = thisTurret.GetComponent<Tower>();
    }

    public void BloodDeducted(int Damage)
    {
        thisTurret.BloodDeducted(Damage);
    }
}
