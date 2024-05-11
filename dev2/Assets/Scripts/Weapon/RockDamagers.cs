using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamagers : EnemyDamagers
{
    [HideInInspector] public float damageAmount;

    [HideInInspector] public float lifeTime, growSpeed = 5f;//武器存在时间
    private Vector3 targetSize;

    public bool shouldKnockBack;
    public float sloweffect;

    public bool destroyParent;//是否要摧毁父类，根据武器类型

    public bool damageOverTime;//是否拥有伤害间隔
    [HideInInspector] public float attackTime;

    private float damageCounter;//伤害间隔时间

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//一次性武器穿透数量
    public float normalAttribute = 1.0f; // 玩家的正常属性值
    public float buffedAttribute = 3.0f; // 增益后的属性值
    private float buffDuration = 10.0f; // 增益持续时间
    [HideInInspector] public float powerattributeTimer; // 攻击属性增益计时器

    public float externalDamage = 0f;
    public float externalScale = 0f;
    public float externalLifeTime = 0f;

    public bool attackBullet = false;
    public bool makeEnemyStone = false;
    public float shootForce = 4.0f;
    public bool shooting;
    public RockDamagers RockPrefabs2;
    void Start()
    {//出现由小变大特效
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//这里先给一个默认的伤害间隔
        if (externalDamage != 0f)
        {
            damageAmount = externalDamage;
        }
        if (externalScale != 0f)
        {
            transform.localScale = Vector3.one * externalScale;
        }
        if (externalLifeTime != 0f)
        {
            lifeTime = externalLifeTime;
        }
        
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//武器存在时间
        if (RockWeapon.instance.weaponLevel == 6&&lifeTime <= 1&&!shooting)
        { 
            shooting=true;
                AttackEnemy();


        }
         

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;//感觉这里是废话
            
            
            Destroy(gameObject);
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                if (attackBullet&& collision.gameObject.GetComponent<EnemyBullet>() != null)
                {
                    
                    Destroy(collision.gameObject);
                }
                
                if (collision.GetComponent<Enemy>() != null)
                {
                    Enemy em = collision.GetComponent<Enemy>();
                    em.TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    if(makeEnemyStone)em.makeStoned();//石化
                }

                destroyOnImpactTimes--;
                if (destroyOnImpactTimes == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (collision.tag == "Enemy")//layerName == "Enemy"
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


    public void addskill()
    {
        attackBullet = true;
    }
    public void makeStone()
    {
        makeEnemyStone = true;
    }
    public void AttackEnemy()
    {
        Vector3 enemyTran = PlayerController.instance.transform.position;
        if (enemyTran == null) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = this.transform.position -enemyTran ;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        InstantiateBullet(this.transform.position, angle);

    }

    void InstantiateBullet(Vector3 position, float angle)
    {
        RockDamagers bullet = Instantiate(RockPrefabs2, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.gameObject.SetActive(true);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
    }
}
