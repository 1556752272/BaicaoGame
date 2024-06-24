using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffMoveSpeedSlow : Buff
{
    public override void RefreshStatus()
    {
        GetComponent<Unit>().GetMoveSpeed();
    }

    //百分比移速加成
    public override float GetModifierMoveSpeedBonus_Percentage()
    {
        return -0.5f;
    }
}
