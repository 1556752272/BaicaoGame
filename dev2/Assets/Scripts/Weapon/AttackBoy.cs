using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoy : MonoBehaviour
{
    [HideInInspector]public bool faceRight = false;
    private float attackRange = 15f; // 宠物的攻击范围  
    public float returnRange = 13f; // 宠物回到玩家身边的范围  
    private float speed =5.0f;
    private float findRange = 10f;
    private Enemy currentTarget; // 当前目标敌人  
    private bool isAttacking = false; // 是否正在攻击 
    // Start is called before the first frame update
    //public List<Vector3> enemiesInRange; // 存储在范围内的敌人


    public float chargeSpeed = 5f; // 冲刺速度  
    //public float chargeDuration = 0.2f; // 冲刺持续时间  
    private bool isCharging = false; // 标记宠物是否正在冲刺  
    private Vector2 chargeDirection; // 冲刺方向  
    private float chargetime;
    private float chargetimer=0.5f;//0.5秒蹦一次
    void Start()
    {
        speed= speed*Random.Range(0.75f, 1.25f);
        
    }

    void Update()
    {
        if (chargetime >= 0)
        {
            chargetime -= Time.deltaTime;
        }
        if (currentTarget != null)//调整宠物方向
        {
            if (this.transform.position.x < currentTarget.transform.position.x)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        }
        
        if (currentTarget == null)
        {
            FindAndAttackNearestEnemy();
        }
        if ((PlayerController.instance.transform.position - transform.position).magnitude > returnRange)
        {

            ReturnToPlayer();
        }
        else if (currentTarget != null) // 如果存在当前目标  
        {
            // 如果当前目标死亡或超出攻击范围，寻找新的目标  
            if ((currentTarget.transform.position - transform.position).magnitude > findRange)
            {
                currentTarget = null;
                FindAndAttackNearestEnemy();
            }
            else
            {

                if (chargetime <= 0)
                {
                    chargetime = chargetimer;
                    AttackCurrentTarget();

                }
                // 否则继续攻击当前目标  
                
            }
        }
    }
    void FindAndAttackNearestEnemy()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(PlayerController.instance.transform.position, attackRange);

        EnemyController nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // 检查敌人标签  
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();
                
                if (enemy != null) // 确保敌人存在且活着  
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                       
                    }
                }
            }
        }

        // 开始攻击最近的敌人  
        if (nearestEnemy != null)
        {
            currentTarget = nearestEnemy;
            isAttacking = true;
            //AttackCurrentTarget();
        }
    }

    void AttackCurrentTarget()
    {
        //if (chargetime <= 0)
        //{
        //    chargetime = chargetimer;


        //}
        // 朝当前目标移动或执行攻击逻辑  
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        //if (direction.x > 0) 
        //{

        //    faceRight = true;
        //}  
        //else
        //{

        //    faceRight = false;
        //}
        //transform.position += direction * Time.deltaTime * speed*2; // 假设宠物以每秒5单位的速度移动  
        Vector3 velocity = direction * chargeSpeed;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void ReturnToPlayer()
    {
        // 朝玩家移动  
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        //if (direction.x > 0)
        //{
        //    faceRight = true;
        //}
        //else
        //{
        //    faceRight = false;
        //}
        Vector3 velocity = direction * chargeSpeed*4f;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        isAttacking = false;
        currentTarget = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

        }
    }
}
