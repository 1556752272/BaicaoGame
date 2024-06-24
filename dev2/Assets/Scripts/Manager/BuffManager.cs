using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //table(victim,damage)
    public static void AddNewBuff(GameObject caster, GameObject target, string buff,float duration)
    {

        System.Type buffType = System.Type.GetType(buff);
        //����Ƕ���buff
        if ((bool)buffType.GetField("isMultiple", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static).GetValue(null))
        {
            Buff newBuff = (Buff)target.AddComponent(buffType);
            newBuff.caster = caster;
            newBuff.duration = duration;
        }
        //���Ƕ���buff
        else
        {
            Buff bufComponent = (Buff)target.GetComponent(buffType);
            //�������buff
            if (bufComponent)
            {
                //����ʱ��
                bufComponent.duration = duration;
                bufComponent.elapsedTime = 0;
                bufComponent.caster = caster;
            }
            else
            {
                //û��buff�����
                Buff newBuff = (Buff)target.AddComponent(buffType);
                newBuff.duration = duration;
                newBuff.caster = caster;
            }
        }



    }
}
