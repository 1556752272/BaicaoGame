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

    public Transform target; // �����Ŀ��λ��
    public PoisonDamager damager;
    public Transform spawnplace;
    private float attackCounter, spawnCounter;
    public float timeBetweenSpawn;
    private float weaponnumber;

    public Transform player; // ��ҵ�Transform����
    public float detectionRadius = 20.0f; // ���뾶
    public LayerMask enemyLayer; // �������ڵĲ�


    public List<EnemyInfo> enemiesInRange; // �洢������Ϣ���б�
    public List<float> enemrange2;

    public float grenadeSpeed = 10f; // ���׵��ٶ�  
    public float flightTime = 1.5f; // ���׵ķ���ʱ��  
    public Transform grenadePrefab; // ���׵�Ԥ����  
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
                    ProcessNearestEnemies(10);//ÿ��20���ӳ�����ƿ��
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
                // �����������ҵľ��룬����ӵ�������Ϣ�б�
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                enemrange2.Add(distance);
                enemiesInRange.Add(new EnemyInfo(hit.transform, distance));
            }
        }
    }    
    void ProcessNearestEnemies(float num)
    {
        // ���ݾ���Ե�����Ϣ�б��������
        enemiesInRange.Sort((a, b) => a.distance.CompareTo(b.distance));

        // ֻ�����������������
        if (enemiesInRange.Count > num)
        {
            enemiesInRange.RemoveRange((int)num, enemiesInRange.Count - (int)num);
        }

        // ����enemiesInRange�����������������
        // ������������Ӵ����߼������緢����������ʾ����
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
            Debug.Log("ɾ����");
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

    // ������Ϣ�࣬���ڴ洢���˵�Transform������ҵľ���
    private void ThrowGrenade(Transform enemy)
    {
        if (enemy == null) return;
        // ����Ͷ���Ƕ�  
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