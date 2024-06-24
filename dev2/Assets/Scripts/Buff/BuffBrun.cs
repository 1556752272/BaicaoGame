using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BuffBrun : Buff
{
    //当buff开始
    public override void OnBuffStart()
    {
        StartCoroutine(ThinkBurn());
    }

    IEnumerator ThinkBurn()
    {
        while (true)
        {
            //造成伤害
            DamageManager.ApplyDamage(new Dictionary<string, dynamic>() {
                { "attacker", caster},
                { "victim", gameObject },
                { "damage", 1 },
            });
            DamageNumberController.instance.SpawnDamage(1, transform.position + new Vector3(0,1,0));
            yield return new WaitForSeconds(0.5f);
        }
    }
}
