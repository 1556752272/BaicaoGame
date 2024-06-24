using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //��λkv
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

    //����
    public void Dying()
    {
        IsAlive = false;//�޸�������ʶ
        GetComponent<Collider2D>().enabled = false;//������ײ��
        StartDeathAnimation();//��ʼ��������
        ExperienceLevelController.instance.SpawnExp(transform.position, 1);//ˢ��ʯ

    }
    //��������
    void StartDeathAnimation()
    {
        //Ĭ��Ϊ��͸��
        StartCoroutine(FadeToTransparent());
    }
    //��͸��Э�̣����ݻ�����
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

            yield return null; // �ȴ���һ֡
        }
        Destroy(gameObject);
    }
    //��ȡ���hp
    public int GetMaxHp()
    {
        //��ȡ��λ����buff
        Buff[] buffs = GetComponents<Buff>();

        int newValue = unitKV["maxHp"];
        // ����buff
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
    //��ȡ�ƶ��ٶ�
    public float GetMoveSpeed()
    {
        //��ȡ��λ����buff
        Buff[] buffs = GetComponents<Buff>();

        float moveSpeedBonus_Percentage = 1;
        // ����buff,��ȡ�ٷֱ����ټӳ�
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