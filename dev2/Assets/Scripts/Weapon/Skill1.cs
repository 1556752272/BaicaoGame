using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime=3f, growSpeed = 5f;//武器存在时间
    private Vector3 targetSize;

    public bool shouldKnockBack;


    public bool destroyParent;//是否要摧毁父类，根据武器类型

    public bool damageOverTime;//是否拥有伤害间隔
    public float attackTime;

    private float damageCounter;//伤害间隔时间

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//一次性武器穿透数量
    public float normalAttribute = 1.0f; // 玩家的正常属性值
    public float buffedAttribute = 3.0f; // 增益后的属性值
    private float buffDuration = 10.0f; // 增益持续时间
    [HideInInspector] public float powerattributeTimer; // 攻击属性增益计时器
    public int sign = 0;
    void Start()
    {//出现由小变大特效
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//这里先给一个默认的伤害间隔

    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//武器存在时间

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;
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
        GameObject collidingObject = collision.gameObject;

        // 获取碰撞体所在的层
        int layerIndex = collidingObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);

        //Debug.Log(damageOverTime);
        //Debug.Log(collision.tag);
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {

                if (collision.GetComponent<Enemy>() != null)
                {   
                    collision.GetComponent<Enemy>().TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    if(sign==1)collision.GetComponent<Enemy>().makeFired(10);

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
}
