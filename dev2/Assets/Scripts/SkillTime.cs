using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTime : MonoBehaviour
{
    [HideInInspector]public float skilltime;
    public float skillCounter=20.0f;//默认技能冷却20秒
    public Image headImage;
    public int sign;
    private float lightnum = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
      //  skilltime = 19.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) &&sign==1 &&skilltime > skillCounter)
        {
            skillStart1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && sign == 2 &&skilltime > skillCounter )
        {
            skillStart2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && sign == 3 && skilltime > skillCounter)
        {
            skillStart3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) &&sign == 4 &&skilltime > skillCounter  )
        {
            skillStart4();
        }
        if (skilltime < skillCounter)
        {
            skilltime += Time.deltaTime;
            headImage.fillAmount = skilltime / skillCounter;
            if (skilltime > skillCounter)
            {
                headImage.fillAmount = 1;
                headImage.color = headImage.color * 2.0f;//恢复亮色，这里还可以调用特效
            }
                
        }
    }

    public void skillStart1()
    {
        if(skilltime > skillCounter)
        {
            skilltime = 0;
            headImage.fillAmount = 0;
            headImage.color = headImage.color*lightnum;
            SkillController.instance.skill1start();
            
        }
        
        //调用技能
    }
    public void skillStart2()
    {
        if (skilltime > skillCounter)
        {
            skilltime = 0;
            headImage.fillAmount = 0;
            headImage.color = headImage.color * lightnum;
            SkillController.instance.skill2start();
        }
    }
    public void skillStart3()
    {
        if (skilltime > skillCounter)
        {
            skilltime = 0;
            headImage.fillAmount = 0;
            headImage.color = headImage.color * lightnum;
            SkillController.instance.skill3start();
        }
    }
    public void skillStart4()
    {
        if (skilltime > skillCounter)
        {
            skilltime = 0;
            headImage.fillAmount = 0;
            headImage.color = headImage.color * lightnum;
            SkillController.instance.skill4start();
        }
    }
}
