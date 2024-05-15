using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPotion : Weapon
{
    public static ThrowPotion instance;
    private void Awake()
    {
        instance = this;
    }

    public Transform target; // 怪物的目标位置
    public PoisonDamager damager;
    public Transform spawnplace;
    private float attackCounter, spawnCounter;
    public float timeBetweenSpawn;
    private float weaponnumber;

    public Transform player; // 玩家的Transform引用
    public float detectionRadius = 20.0f; // 检测半径
    public LayerMask enemyLayer; // 敌人所在的层


    public List<EnemyInfo> enemiesInRange; // 存储敌人信息的列表
    public List<float> enemrange2;

    public float grenadeSpeed = 10f; // 手雷的速度  
    public float flightTime = 1.5f; // 手雷的飞行时间  
    public Transform grenadePrefab; // 手雷的预制体  
    public BezierThrow bezierPrefab;

    public bool throwlots;
    public float throwTime = 1f;
    void Start()
    {
        damager.getbigger = false;
        throwlots = false;
        SetStats();
        weaponnumber = 3;

    }
    private void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            if (weaponLevel == 0)
            {
                return;
            }
            DetectEnemiesInRange();

             ProcessNearestEnemies(weaponnumber);
            
        }
        if (throwlots)
        {
            if (throwTime > 0)
            {
                throwTime -= Time.deltaTime;
                if (throwTime <= 0)
                {
                    throwTime = 20f;
                    ProcessNearestEnemies(10);//每隔20秒扔出大量瓶子
                }
            }
        }
       
    }    
    void DetectEnemiesInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        //Debug.Log(enemiesInRange.Count);
        if(enemiesInRange.Count!=0) enemiesInRange.Clear();

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // 计算敌人与玩家的距离，并添加到敌人信息列表
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                enemrange2.Add(distance);
                enemiesInRange.Add(new EnemyInfo(hit.transform, distance));
            }
        }
    }    
    void ProcessNearestEnemies(float num)
    {
        // 根据距离对敌人信息列表进行排序
        enemiesInRange.Sort((a, b) => a.distance.CompareTo(b.distance));

        // 只保留最近的三个敌人
        if (enemiesInRange.Count > num)
        {
            enemiesInRange.RemoveRange((int)num, enemiesInRange.Count - (int)num);
        }

        // 现在enemiesInRange包含最近的三个敌人
        // 可以在这里添加处理逻辑，例如发射武器或显示警告
        for(int i = 0; i < num; i++)
        {
            if (i + 1 > enemiesInRange.Count) break;
            ThrowPotionAtTarget(enemiesInRange[i]);
        }
    }
    void ThrowPotionAtTarget(EnemyInfo ene)
    {
        SFXManager.instance.PlaySFXPitched(4);
        ThrowGrenade(ene.enemyTransform);
    }
    public void SetStats()
    {
        if (weaponLevel == 5)
        {
            damager.getbigger = true;
        }
        if (weaponLevel == 6)
        {
            throwlots = true;
        }
        Debug.Log(weaponLevel);
        if (weaponLevel == 6)
        {
            Debug.Log("删除？");
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.damageAmount = stats[weaponLevel].damage;
        damager.attackTime = stats[weaponLevel].speed;
        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;
        weaponnumber = stats[weaponLevel].amount;
        damager.sloweffect= stats[weaponLevel].slowEffects;
        spawnCounter = 0f;
    }

    List<Transform> GetEnemiesInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.position, detectionRadius, enemyLayer);
        List<Transform> enemies = new List<Transform>();

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemies.Add(hit.transform);
            }
        }

        return enemies;
    }

    // 敌人信息类，用于存储敌人的Transform和与玩家的距离
    private void ThrowGrenade(Transform enemy)
    {
        if (enemy == null) return;
        // 计算投掷角度  
        Vector2 direction = enemy.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        BezierThrow pre = Instantiate(bezierPrefab, transform.position, Quaternion.identity);
        pre.target = enemy;
        pre.Init(this.gameObject);
        
    }
}

[System.Serializable]
 public class EnemyInfo
    {
        public Transform enemyTransform;
        public float distance;

        public EnemyInfo(Transform enemyTransform, float distance)
        {
            this.enemyTransform = enemyTransform;
            this.distance = distance;
        }
}