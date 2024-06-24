using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float duration;//持续时间
    public float elapsedTime;//已用时间
    static public bool isMultiple;//是否多次添加
    public GameObject caster;//施法者


    //刷新状态
    public virtual void RefreshStatus() { }
    //当buff开始
    public virtual void OnBuffStart() { }




    //固定hp加成
    //GetComponent<Unit>().getMaxHp();
    public int GetModifierHealthBonus()
    {
        return 0;
    }
    //百分比移速加成
    //GetComponent<Unit>().getMoveSpeed();
    public virtual float GetModifierMoveSpeedBonus_Percentage()
    {
        return 0;
    }

    //计时销毁
    IEnumerator TimeToDestroy()
    {
        while (true)
        {
            float tick = 0.1f;
            yield return new WaitForSeconds(tick);
            elapsedTime += tick;
            if (duration > 0 && elapsedTime >= duration)
            {
                Destroy(this);
            }
        }
    }


    //启用和销毁时刷新状态
    void Start()
    {
        RefreshStatus();
        StartCoroutine(TimeToDestroy());//计时销毁
        OnBuffStart();
    }
    void OnDestroy()
    {
        enabled = false;
        RefreshStatus();
    }
    void OnEnable()
    {
        try { RefreshStatus(); } catch (Exception ex) { }
    }
    void OnDisable()
    {
        RefreshStatus();
    }




}
