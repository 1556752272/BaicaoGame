using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyWeapon : Weapon
{
    public static BoyWeapon instance;
    public BoyDamagers damager;
    public Transform player; // ��ҵ�Transform����  
    //public float attackRange = 2f; // ����Ĺ�����Χ  
    //public float returnRange = 14f; // ����ص������ߵķ�Χ  
    //public LayerMask enemyLayer; // ���˵�ͼ������  

    //private GameObject currentTarget; // ��ǰĿ�����  
    //private bool isAttacking = false; // �Ƿ����ڹ���  

    public Transform Spawnplace;
    private float attackCounter, timeBetweenSpawn, direction;
    public float weaponnumber;
    private int petnum = 1;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
           
        }
    }
        void SetStats()
        {
        if (weaponLevel == 1)//����һ����ʱ���������
        {
            BoyDamagers b = Instantiate(damager, Spawnplace.position, damager.transform.rotation);
            PlayerHealthController.instance.boy = b;
        }
        if (weaponLevel == 6)
        {
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.damageAmount = stats[weaponLevel].damage;
            damager.lifeTime = 20000;//�����Զ������ʧ

            damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
            //timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
           
            damager.destroyOnImpactTimes = (int)stats[weaponLevel].acrossEnemyNums;

            attackCounter = 0f;
        }
    
}

