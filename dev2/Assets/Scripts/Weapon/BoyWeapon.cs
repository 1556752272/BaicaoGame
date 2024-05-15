using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyWeapon : Weapon
{
    public static BoyWeapon instance;
    public BoyDamagers damager;
    public Transform player; // 玩家的Transform引用  
    //public float attackRange = 2f; // 宠物的攻击范围  
    //public float returnRange = 14f; // 宠物回到玩家身边的范围  
    //public LayerMask enemyLayer; // 敌人的图层掩码  

    //private GameObject currentTarget; // 当前目标敌人  
    //private bool isAttacking = false; // 是否正在攻击  

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
        if (weaponLevel == 1)//设置一级的时候产生海兔
        {
            BoyDamagers b = Instantiate(damager, Spawnplace.position, damager.transform.rotation);
            PlayerHealthController.instance.boy = b;
        }
        if (weaponLevel == 6)
        {
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.damageAmount = stats[weaponLevel].damage;
            damager.lifeTime = 20000;//最好永远不会消失

            damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
            //timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
           
            damager.destroyOnImpactTimes = (int)stats[weaponLevel].acrossEnemyNums;

            attackCounter = 0f;
        }
    
}

