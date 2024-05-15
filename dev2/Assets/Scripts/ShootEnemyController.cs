using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Spine.Unity;
public class ShootEnemyController : Enemy
{
    [HideInInspector] public Rigidbody2D theRB;
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
    [HideInInspector] public Transform bulletSpawnPoint; // 子弹生成点
    
    public float attackRange = 5.0f; // 怪物的攻击射程
    public float attackCooldown = 1.0f; // 攻击冷却时间

    [HideInInspector]public bool faceRight = false; 
    
    public float shootForce = 5.0f; // 射击力
    private float attackTimer; // 攻击计时器
    public bool isSlow;
    public float slowNumber;
    private int stoneattacktimer;
    private bool stoned;
    private float stonedTimer;
    // private Rigidbody2D rb; // 怪物的Rigidbody2D组件
    private bool isIgnited = false; // 是否被点燃  
    private Coroutine igniteCoroutine; // 点燃协程  
    public SkeletonAnimation spineAnimation;
    //public bool isWalking = true;
    private float Hurttimer;
    private MeshRenderer meshRenderer;
    private Color mineColor;
    void Start()
    {
        maxhealth = health;
        bulletSpawnPoint = this.transform;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
        attackTimer = -1; // 初始化攻击计时器
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        mineColor = meshRenderer.material.color;
    }

    void Update()
    {
        if (Hurttimer >= 0)
        {
            Hurttimer -= Time.deltaTime;
            if (Hurttimer <= 0)
            {
                meshRenderer.material.color = mineColor;
            }
        }
        if ((PlayerController.instance.transform.position.x < transform.position.x)&&faceRight)
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
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
            if (PlayerController.instance.gameObject.activeSelf == true)//这下面一整个都是关于移动的
            {
                // 检测玩家是否在射程内
                if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                {
                    // 停止怪物移动
                    theRB.velocity = Vector2.zero;

                    // 每经过攻击冷却时间，进行一次攻击
                    if (attackTimer <= 0)
                    {
                        AttackPlayer();
                        attackTimer = attackCooldown;
                        spineAnimation.AnimationName = "attack";
                    spineAnimation.timeScale = 0.5f;
                        //isWalking = false; 
                        spineAnimation.loop = false; 
                    }
                    
                }
                else
                {
                    // 玩家不在射程内，怪物向玩家移动
                    //Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;
                    //theRB.velocity = direction * moveSpeed;
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
                        spineAnimation.AnimationName = "walk";
                        spineAnimation.loop = true;
                        spineAnimation.timeScale = 1f;
                    // if (!isWalking)
                    // {isWalking = true;  }

                }
                    if (isSlow) theRB.velocity *= slowNumber;
                    if (hitCounter >= 0f)
                    {
                        hitCounter -= Time.deltaTime;
                    }
                }
            
        }
}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter < 0f)
        {
            knockBackCounter = 0.2f * knockBackTime;//这里我希望怪物碰到角色后反弹一点距离

            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
        if (collision.gameObject.tag == "Player" && hitCounter >= 0f)
        {
            knockBackCounter = 0.1f * knockBackTime;//这里我希望怪物碰到角色后反弹一点距离

        }

    }

    public override void TakeDamage(float damageToTake)
    {
        //meshRenderer.material.color = Color.white;
        //StartCoroutine(ChangeColorToWhite(0.3f));
        meshRenderer.material.color = Color.gray;
        Hurttimer = 1f;
        SFXManager.instance.PlaySFXPitched(2);
        health -= damageToTake;
        if(health <= 0)
        {

            //Destroy(gameObject);   
            ExperienceLevelController.instance.SpawnExp(transform.position,expToGive);
            if (isFading == false) StartCoroutine(FadeOut());


            if (Random.value <= coinDropRate)//一半概率掉落金币
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            
           // SFXManager.instance.PlaySFXPitched(0);
        }else
        {
          //  SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
        //Debug.Log("我倒要看看在哪: " + transform.position);

    }
    bool isFading = false;
    IEnumerator FadeOut()
    {
        isFading = true;
        Color fadeColor = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 0); // 透明色  
        float elapsedTime = 0;

        while (elapsedTime < 1f && isFading)
        {
            // 使用Lerp在elapsedTime和fadeDuration之间插值  
            meshRenderer.material.color = Color.Lerp(mineColor, fadeColor, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                Die();
                ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
                if (Random.value <= coinDropRate)//一半概率掉落金币
                {
                    CoinController.instance.DropCoin(transform.position, coinValue);
                }
                Destroy(this.gameObject);
            }
            yield return null; // 等待直到下一帧  
        }

        
    }
    IEnumerator ChangeColorToWhite(float duration)
    {
        // 使用Color.Lerp在duration时间内将颜色混合到白色  
        float lerpTime = 0f;
        Color startColor = meshRenderer.material.color; // 当前颜色可能已经被其他逻辑改变  

        while (lerpTime < duration)
        {
            lerpTime += Time.deltaTime;
            Color color = Color.Lerp(startColor, Color.black, lerpTime / duration);
            meshRenderer.material.color = color;
            yield return null; // 等待下一帧  
        }

        // 恢复原始颜色（如果需要的话）  
        meshRenderer.material.color = mineColor;
    }
    public override void TakeDamage(float damageToTake,bool shouldKnockback)//检测武器是否有击退特效
    {
        if (poison) { TakeDamage(damageToTake * 1.3f); } //这里是弓箭上毒素加倍伤害
        else { TakeDamage(damageToTake); }
        if (shouldKnockback) {
            knockBackCounter = knockBackTime;
        }
    }
    public override void TakeDamage(float damageToTake, int fire)
    {

        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            
            if(isFading==false)StartCoroutine(FadeOut());

            

        }
        else
        {
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position, true);

    }
    public void AttackPlayer()
    {
        //if (PlayerController.instance.transform.position.x < transform.position.x) { //左侧射击玩家
        //// 实例化子弹
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position - new Vector3(0.3f, 0, 0), Quaternion.identity);

        //// 计算子弹的方向向量，指向玩家
        //Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
        //direction.Normalize(); // 确保方向向量的长度为1

        //// 为子弹添加速度和方向
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = direction * shootForce;
        //}
        //else
        //{
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // 确保方向向量的长度为1
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position+new Vector3(0.3f,0,0), Quaternion.Euler(new Vector3(0, 0, angle)));


            // 为子弹添加速度和方向
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
       // }
        //Vector3 direction = PlayerController.instance.transform.position-this.transform.position;
        //direction.Normalize(); // 确保方向向量的长度为1
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        //BulletDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        //bullet.gameObject.SetActive(true);
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
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
        //DestroyMonster();
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
    public void DestroyMonster()
    {
        // 开始渐变效果  
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        Color fadeOutColor = Color.black;//new Color(mineColor.r, mineColor.g, mineColor.b, 0f); // 透明颜色  
        float fadeDuration = 1f; // 渐变持续时间  

        // 使用Color.Lerp进行颜色渐变  
        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeDuration)
        {
            Color color = Color.Lerp(mineColor, fadeOutColor, t);
            meshRenderer.material.color = color;

            // 等待下一帧  
            yield return null;
        }

        // 渐变完成后销毁怪物  
        Destroy(gameObject);
    }
}
