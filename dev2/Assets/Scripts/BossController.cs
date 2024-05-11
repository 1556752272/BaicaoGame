using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Enemy
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;//怪物伤害

    public float hitWaitTime = 1f;//碰撞角色下次的冷却时间
    private float hitCounter;



    public float knockBackTime = 0.5f;//击退怪物时间
    private float knockBackCounter;

    public int expToGive = 1;//掉落经验

    public int coinValue = 1;//掉落金币数量
    public float coinDropRate = .5f;//掉落金币概率
                                    //*********************************************************

    public GameObject bulletPrefab; // 子弹Prefab
    public Transform bulletSpawnPoint; // 子弹生成点

    public float shootCooldown = 2.0f; // 射击冷却时间
    public float chargeCooldown = 3.0f; // 冲撞冷却时间
    public float chargetime;//冲撞计时器和判断
    public float chargeCounter = 2.0f;
    public Vector3 chargeDirection;
    public float chargeSpeed = 10.0f; // 冲撞速度
    private float shootTimer; // 射击计时器
    private float chargeTimer; // 冲撞计时器
    public float attackRange = 20.0f; // 怪物的攻击射程
    //public float attackCooldown = 1.0f; // 攻击冷却时间

    public bool faceRight = false;

    public float shootForce = 5.0f; // 射击力

    private int stoneattacktimer;
    private bool stoned;
    float stonedTimer;

    private bool isIgnited = false; // 是否被点燃  
    private Coroutine igniteCoroutine; // 点燃协程  
    void Start()
    {
        maxhealth = health;
        bulletSpawnPoint = this.transform;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
        //attackTimer = attackCooldown; // 初始化攻击计时器
        shootTimer = shootCooldown;
        chargeTimer = chargeCooldown;
    }

    void Update()
    {
        if ((PlayerController.instance.transform.position.x < transform.position.x) && faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = false;
        }
        if ((PlayerController.instance.transform.position.x > transform.position.x) && !faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = true;
        }
        if (stonedTimer > 0)
        {
            stonedTimer -= Time.deltaTime;
            if (stonedTimer <= 0)
            {
                stoned = false;
            }
        }
        if (stonedTimer > 0)
        {
            stonedTimer -= Time.deltaTime;
            this.theRB.velocity = Vector3.zero;
            if (stonedTimer <= 0)
            {
                stoned = false;
            }
        }
        
            if (PlayerController.instance.gameObject.activeSelf == true)//这下面一整个都是关于移动的
            {
                //// 检测玩家是否在射程内
                //if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                //{
                //    // 停止怪物移动
                //    theRB.velocity = Vector2.zero;

                //    // 每经过攻击冷却时间，进行一次攻击
                //    if (attackTimer <= 0)
                //    {
                //        AttackPlayer();
                //        attackTimer = attackCooldown;
                //    }
                //    else
                //    {
                //        attackTimer -= Time.deltaTime;
                //    }
                //if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                // {
                // 每经过射击冷却时间，进行一次射击
                if (shootTimer <= 0)
                {
                    AttackPlayer();//射击玩家使用某种招式
                    shootTimer = shootCooldown;
                }
                else
                {
                    shootTimer -= Time.deltaTime;
                }
                //}


                // 每经过冲撞冷却时间，进行一次冲撞
                if (chargeTimer <= 0)
                {

                    chargeDirection = (target.position - transform.position).normalized;
                    chargetime = chargeCounter;//更改冲刺时间和方向
                                               //ChargeAtPlayer();
                    chargeTimer = chargeCooldown;
                }
                else
                {
                    chargeTimer -= Time.deltaTime;
                }
            if (stoned)
            {
                theRB.velocity = Vector3.zero;
            }
            else
            {
                bossmove();
            }

            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter < 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }

    }

    public override void TakeDamage(float damageToTake)
    {
        SFXManager.instance.PlaySFXPitched(2);
        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);

            Destroy(gameObject);

            if (Random.value <= coinDropRate)//一半概率掉落金币
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

            // SFXManager.instance.PlaySFXPitched(0);
        }
        else
        {
            //  SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
        //Debug.Log("我倒要看看在哪: " + transform.position);

    }

    public override void TakeDamage(float damageToTake, bool shouldKnockback)//检测武器是否有击退特效
    {
        if (poison) { TakeDamage(damageToTake * 1.3f); } //这里是弓箭上毒素加倍伤害
        else { TakeDamage(damageToTake); }
        if (shouldKnockback)//我认为boss不该被击退
        {
            knockBackCounter = knockBackTime;
        }
    }
    public override void TakeDamage(float damageToTake, int fire)
    {
        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            Die();
            Destroy(gameObject);

            if (Random.value <= coinDropRate)//一半概率掉落金币
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

        }
        else
        {
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position, true);

    }
    public void AttackPlayer()
    {
        if (PlayerController.instance.transform.position.x < transform.position.x)
        { //左侧射击玩家
          // 实例化子弹
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position - new Vector3(0.3f, 0, 0), Quaternion.identity);

            // 计算子弹的方向向量，指向玩家
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // 确保方向向量的长度为1

            // 为子弹添加速度和方向
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * shootForce;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + new Vector3(0.3f, 0, 0), Quaternion.identity);

            // 计算子弹的方向向量，指向玩家
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // 确保方向向量的长度为1

            // 为子弹添加速度和方向
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * shootForce;
        }
    }
    private void bossmove()
    {
        if (chargetime >= 0)
        {
            chargetime -= Time.deltaTime;
            theRB.velocity = chargeDirection * chargeSpeed;
            return;
        }
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
        if (stoned)
        {
            theRB.velocity = Vector3.zero;
        }
        else
        {
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
        }

        if (hitCounter >= 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }
    void ChargeAtPlayer()
    {
        // 这里应该包含检测玩家位置的逻辑，并朝玩家冲撞
        // 简化示例中，我们假设Boss沿着正方向冲撞
        Vector3 direc = (target.position - transform.position).normalized;
        //// 添加冲撞力
        //theRB.AddForce(transform.forward * chargeSpeed, ForceMode2D.Impulse);
        theRB.velocity = direc * chargeSpeed;
    }
    public override void makeStoned()
    {
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 0;
            stoned = true;
            stonedTimer = 2f;
        }
    }
    public override void makemaxStoned()
    {
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 5;
            stoned = true;
            stonedTimer = 2f;
        }
    }

    public override void makeFired(int num)
    {
        igniteDamage = num;
        if (!isIgnited)
        {
            isIgnited = true;
            igniteCoroutine = StartCoroutine(TakeIgniteDamage());
        }
    }

    // 每半秒掉血的协程  
    private IEnumerator TakeIgniteDamage()
    {
        while (isIgnited && health > 0)
        {
            yield return new WaitForSeconds(igniteInterval);
            TakeDamage(igniteDamage, 2);
        }
    }

    // 怪物死亡逻辑  
    private void Die()
    {
        if (isIgnited)
        {
            isIgnited = false;
            StopCoroutine(igniteCoroutine);

            // 检测并伤害周围的敌人  
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                Enemy enemy = col.GetComponent<Enemy>(); // 假设你有一个Enemy脚本在敌人身上  
                if (enemy != null)
                {
                    enemy.TakeDamage(deathDamage,2);
                }
            }
        }

    }
}
