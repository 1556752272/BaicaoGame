using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : EnemyDamagers
{
    public PlayerBubble dad;
    [HideInInspector] public float damageAmount;

    [HideInInspector] public float lifeTime, growSpeed = 5f;//武器存在时间
    private Vector3 targetSize;

    public bool shouldKnockBack;


    public bool destroyParent;//是否要摧毁父类，根据武器类型

    public bool damageOverTime;//是否拥有伤害间隔
    [HideInInspector] public float attackTime;

    private float damageCounter;//伤害间隔时间，不应该有这个

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//一次性武器穿透数量
    public float normalAttribute = 1.0f; // 玩家的正常属性值
    public float buffedAttribute = 3.0f; // 增益后的属性值
    private float buffDuration = 10.0f; // 增益持续时间
    [HideInInspector] public float powerattributeTimer; // 攻击属性增益计时器
    private float searchRadius = 24.0f; // 搜索半径
    [HideInInspector] public List<Vector3> enemiesInLittleRange;
    public EnemyDamager littleBubble;
    public float shootForce = 3.0f; // 射击力
    void Start()
    {//出现由小变大特效
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//这里先给一个默认的伤害间隔
        dad = PlayerBubble.instance;

    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//武器存在时间

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0)
            {
                damageCounter = attackTime;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                            enemiesInRange[i].TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                        

                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
    void FindEnemiesInRange()//更新附近敌人坐标
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, searchRadius);

        enemiesInLittleRange.Clear();

        foreach (Collider2D hit in hits)
        {
            // 确保是敌人并且不是主角自己
            if (hit.CompareTag("Enemy") && hit.transform != this.transform)
            {
                enemiesInLittleRange.Add(new Vector3(hit.transform.position.x, hit.transform.position.y, 0));//enemiesInRange.Add(hit.transform);
            }
        }
    }
    public Vector3 GetClosestEnemy()
    {
        Vector3 closestEnemy = Vector3.zero;
        float shortestDistance = float.MaxValue;

        foreach (Vector3 enemy in enemiesInLittleRange)
        {
            // if (enemy == null) continue;//如果死了就跳过，但我觉得每个时刻都在更新最近敌人位置，我觉得有点无所谓
            float distance = Vector2.Distance(this.transform.position, enemy);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;//返回最近敌人坐标
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;

        // 获取碰撞体所在的层
        int layerIndex = collidingObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);



        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<Enemy>() != null)
                {
                    collision.GetComponent<Enemy>().TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    if (collision.GetComponent<Enemy>().health - damageAmount <= 0)
                    {
                        PlayerBubble.instance.addKillnum();
                    }
                }
                

                
                destroyOnImpactTimes--;//弹射几次
                if (PlayerBubble.instance.weaponLevel >= 6)
               {
                    FindEnemiesInRange();
                    Vector3 v = GetClosestEnemy();
                    AttackEnemy();

                }
                
                if (destroyOnImpactTimes == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    //Transform enemyTran;
                    Vector3 enemyTran=Vector3.zero; //Transform enemyTran2 ; ;enemyTran2.position = new position;
                    List<Vector3> enemies = PlayerController.instance.enemiesInRange;
                    if (enemies.Count > 0)
                    {
                        int nextIndex = Random.Range(0, enemies.Count);
                        enemyTran = enemies[nextIndex];
                    }
                    //else
                    //{
                    //    // 如果没有敌人，可以停止泡泡或做其他处理  
                    //    enemyTran =Vector3.zero;
                    //}
                    //Transform enemyTran = PlayerController.instance.GetClosestEnemy();
                    if (enemyTran== Vector3.zero) Destroy(gameObject);//没有敌人泡泡则自己破裂
                    // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);


                    Vector3 direction = enemyTran - this.transform.position; //enemyTran.position - this.transform.position;
                    direction.Normalize(); // 确保方向向量的长度为1

                    // 为子弹添加速度和方向
                    Rigidbody2D bulletRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
                    
                    bulletRigidbody.velocity = direction * dad.shootForce;
                }
            }
        }
        else
        {
            if (layerName == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<Enemy>());
            }
        }
    }
    public void powertimeup()//食物强化
    {
        powerattributeTimer = buffDuration;
    }
    public void AttackEnemy()
    {
        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == null) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = enemyTran - PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        EnemyDamager bullet = Instantiate(littleBubble, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        bullet.gameObject.SetActive(true);
        // 为子弹添加速度和方向
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * shootForce;

    }
}
