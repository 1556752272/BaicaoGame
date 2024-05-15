using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyDamagers :EnemyDamagers
{
    [HideInInspector] public float damageAmount;

    [HideInInspector] public float lifeTime, growSpeed = 5f;//��������ʱ��
    private Vector3 targetSize;

    public bool shouldKnockBack;
    public float sloweffect;

    public bool destroyParent;//�Ƿ�Ҫ�ݻٸ��࣬������������

    public bool damageOverTime;//�Ƿ�ӵ���˺����
    [HideInInspector] public float attackTime;

    private float damageCounter;//�˺����ʱ��

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//һ����������͸����
    public float normalAttribute = 1.0f; // ��ҵ���������ֵ
    public float buffedAttribute = 3.0f; // ����������ֵ
    private float buffDuration = 10.0f; // �������ʱ��
    [HideInInspector] public float powerattributeTimer; // �������������ʱ��

    public float externalDamage = 0f;
    public float externalScale = 0f;
    public float externalLifeTime = 0f;
    public bool poison = false;
    public int eatnumber = 0;
    private float attackPoint;
    private float skillTimer=5.0f;
    public float madTimer = -1f;
    void Start()
    {//������С�����Ч
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//�����ȸ�һ��Ĭ�ϵ��˺����
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
        attackPoint = 0;
        
    }

    void Update()
    {
        if (eatnumber >= 30)
        {
            this.transform.localScale = this.transform.localScale * 1.2f;//�����Ϊ1.2��
            if (BoyWeapon.instance.weaponLevel >= 6)
            {
                PlayerHealthController.instance.AddHealth(10) ;
                
            }
        }
        if (madTimer > 0)
        {
            madTimer -= Time.deltaTime;
            if (madTimer <= 0)//��񻯽���
            {
                attackTime *= 2f;
                damageAmount *= 0.5f;
                this.transform.localScale *= 0.5f;
            }
        }
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//��������ʱ��
        if (attackPoint >= 100)
        {
            attackPoint -= 100;
            PlayerHealthController.instance.addHealth(1);
        }
        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;//�о������Ƿϻ�
            Destroy(gameObject);
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        skillTimer += Time.deltaTime;
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
                            if (BoyWeapon.instance.weaponLevel >= 3 && skillTimer >= 5f)
                            {
                                skillTimer = 0f;
                                //EatEnemy(enemiesInRange[0]);
                            }
                            enemiesInRange[i].TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);//�����ƺ��п���Խ��
                            attackPoint +=damageAmount;
                        

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
        GameObject collidingObject = collision.gameObject;

        // ��ȡ��ײ�����ڵĲ�
        int layerIndex = collidingObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);

        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<Enemy>() != null)
                {
                    Enemy em = collision.GetComponent<Enemy>();
                    em.TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
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
    public void powertimeup()//ʳ��ǿ��
    {
        powerattributeTimer = buffDuration;
    }

    public void EatEnemy(Enemy enemy)
    {
        EnemyController ene = enemy.gameObject.GetComponent<EnemyController>();
        if (ene != null)
        {
            Destroy(ene.gameObject);
            eatnumber++;
            
        }
        ShootEnemyController ene2 = enemy.gameObject.GetComponent<ShootEnemyController>();
        if (ene2 != null)
        {
            Destroy(ene2.gameObject);
            eatnumber++;
            
        }
        BossController ene3 = enemy.gameObject.GetComponent<BossController>();
        if (ene3 != null)
        {
            ene3.TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
        }
    }
    public void powerup()//���
    {

        if (BoyWeapon.instance.weaponLevel < 5)
        {
            
            return;//û�дﵽ������ֱ�ӽ���
        }
        else
        {
            if (madTimer <= 0f)
        {
                
            madTimer = 5f;
            attackTime *= 0.5f;
            damageAmount *= 2;
            this.transform.localScale *= 2f;
        }
        else
        {
            madTimer = 5f;
        }

        }
        
        
    }
}
