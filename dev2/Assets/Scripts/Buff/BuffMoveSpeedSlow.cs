using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffMoveSpeedSlow : Buff
{
    public override void RefreshStatus()
    {
        Debug.Log("ˢ��״̬");
        GetComponent<Unit>().GetMoveSpeed();
    }

    //�ٷֱ����ټӳ�
    public override float GetModifierMoveSpeedBonus_Percentage()
    {
        return -0.5f;
    }
}
