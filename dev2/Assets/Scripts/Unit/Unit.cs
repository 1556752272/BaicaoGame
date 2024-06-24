using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //单位kv
    public int maxHp = 100;
    public float moveSpeed = 2;

    public int hp = 100;
    public bool IsAlive = true;
    public Dictionary<string, dynamic> unitKV = new Dictionary<string, dynamic>();


    void Awake()
    {
        hp = maxHp;
        unitKV["maxHp"] = maxHp;
        unitKV["moveSpeed"] = moveSpeed;
    }

    void Update()
    {
        if (hp <= 0 && IsAlive)
        {
            Dying();
        }
    }

    //死亡
    public void Dying()
    {
        IsAlive = false;//修改死亡标识
        GetComponent<Collider2D>().enabled = false;//禁用碰撞体
        StartDeathAnimation();//开始死亡动画
        ExperienceLevelController.instance.SpawnExp(transform.position, 1);//刷宝石

    }
    //死亡动画
    void StartDeathAnimation()
    {
        //默认为变透明
        StartCoroutine(FadeToTransparent());
    }
    //变透明协程，最后摧毁自身
    IEnumerator FadeToTransparent()
    {
        float elapsedTime = 0;
        float fadeDuration = 0.3f;

        Renderer objectRenderer = GetComponent<Renderer>();
        Color color = objectRenderer.material.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            color.a = alpha;
            objectRenderer.material.color = color;

            yield return null; // 等待下一帧
        }
        Destroy(gameObject);
    }
    //获取最大hp
    public int GetMaxHp()
    {
        //获取单位所有buff
        Buff[] buffs = GetComponents<Buff>();

        int newValue = unitKV["maxHp"];
        // 遍历buff
        foreach (Buff buff in buffs)
        {
            if (buff.enabled)
            {
                //Debug.Log(buff.GetModifierHealthBonus());
                newValue += buff.GetModifierHealthBonus();
            }
        }
        maxHp = newValue;

        return newValue;
    }
    //获取移动速度
    public float GetMoveSpeed()
    {
        //获取单位所有buff
        Buff[] buffs = GetComponents<Buff>();

        float moveSpeedBonus_Percentage = 1;
        // 遍历buff,获取百分比移速加成
        foreach (Buff buff in buffs)
        {
            if (buff.enabled)
            {
                moveSpeedBonus_Percentage *= 1+buff.GetModifierMoveSpeedBonus_Percentage();
            }
        }
        moveSpeed = unitKV["moveSpeed"]* moveSpeedBonus_Percentage;

        return moveSpeed;
    }

}