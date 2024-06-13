using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
//using DragonBones;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    public float moveSpeed;

    //public Animator anim;

    public float pickupRange = 1;

    public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignWeapons;
    public int maxWeapons;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();
    public float normalAttribute=1.0f; // 玩家的正常属性值
    public float buffedAttribute=3.0f; // 增益后的属性值
    public float buffDuration = 10.0f; // 增益持续时间
    public float powerattributeTimer; // 攻击属性增益计时器
    public float speedattributeTimer; // 速度属性增益计时器
    public float magnetattributeTimer; // 捡拾属性增益计时器
    public LayerMask coinLayer; // 金币所在的层
    public LayerMask ExpLayer; // 所在的层
    public float detectionRadius = 20.0f; // 磁力检测半径
    private List<CoinPickup> coinAll = new List<CoinPickup>(); // 存储范围内的金币
    private List<ExpPickup> exps = new List<ExpPickup>();


    private float searchRadius = 24.0f; // 搜索半径
                                       
    private float findenemytime;
    [HideInInspector]public List<Vector3> enemiesInRange; // 存储在范围内的敌人


   // public UnityDragonBones dragonBonesComponent; // 假设你使用的是DragonBones的Unity插件  

    // 动画名称  
    private string idleAnimationName = "idle";
    private string walkRightAnimationName = "walk_right";
    private string walkLeftAnimationName = "walk_left"; // 如果使用翻转，则可能不需要这个  
    private string deathAnimationName = "death";

    private bool isDead = false;
    void Start()
    {
        


        enemiesInRange = new List<Vector3>();
        if (assignWeapons.Count == 0)
        {
            //AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
        //状态开局从状态机PlayerStatController中获取
        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);
    }

    // Update is called once per frame
    void Update()
    {
        
        pickcoinAndExp();
        //if (moveInput != Vector3.zero)
        //{
        //    anim.SetBool("isMoving", true);
        //}
        //else
        //{
        //    anim.SetBool("isMoving", false);
        //}
        findenemytime -= Time.deltaTime;
        if (findenemytime < 0)
        {
            FindEnemiesInRange();
            findenemytime = 0.5f;
        }
        
    }
    private void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if(PlayerHealthController.instance.currentHealth > 0) { 
            if (speedattributeTimer > 0)
            {//移动增幅
                speedattributeTimer -= Time.fixedDeltaTime;
                transform.position += moveInput * moveSpeed * Time.fixedDeltaTime * buffedAttribute;
            }
            else
            {
                transform.position += moveInput * moveSpeed * Time.fixedDeltaTime;
            }
        }
    }
    public void magnetfunc()
    {
        magnetattributeTimer = buffDuration;
    }
    public void pickcoinAndExp()
    {
        if (magnetattributeTimer > 0)
        {
            magnetattributeTimer -= Time.deltaTime;
            Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, detectionRadius, coinLayer);
            Collider2D[] Exps = Physics2D.OverlapCircleAll(transform.position, detectionRadius, ExpLayer);
            // 将检测到的金币添加到列表中
            foreach (Collider2D coin in coins)
            {
                CoinPickup c = coin.GetComponent<CoinPickup>();//如果还想让他做什么
                coinAll.Add(c);
                c.movingToPlayer = true;
                c.moveSpeed *= 3;
            }
            foreach (Collider2D e in Exps)
            {
                ExpPickup ex= e.GetComponent<ExpPickup>();//如果还想让他做什么
                exps.Add(ex);
                ex.movingToPlayer = true;
                ex.moveSpeed *= 3;
            }
        }
    }
    void FindEnemiesInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, searchRadius);

        enemiesInRange.Clear();

        foreach (Collider2D hit in hits)
        {
            // 确保是敌人并且不是主角自己
            if (hit.CompareTag("Enemy") && hit.transform != this.transform)
            {
                enemiesInRange.Add(new Vector3(hit.transform.position.x, hit.transform.position.y,0));//enemiesInRange.Add(hit.transform);
            }
        }
    }

    // 找到并返回距离最短的敌人
    public Vector3 GetClosestEnemy()
    {
        Vector3 closestEnemy = Vector3.zero;
        float shortestDistance = float.MaxValue;

        foreach (Vector3 enemy in enemiesInRange)
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
    public GameObject ch2skill;
    public Weapon ch2weapon1;
    public Weapon ch2weapon2;
    public GameObject ch2sprite;
    public GameObject ch2button;
    public void ch2Join()
    {
        ch2skill.SetActive(true);
        ch2weapon1.gameObject.SetActive(true);
        ch2weapon2.gameObject.SetActive(true);
        ch2sprite.SetActive(true);
        assignWeapons.Add(ch2weapon1);
        assignWeapons.Add(ch2weapon2);
        ch2button.SetActive(true);
    }
    public GameObject ch3skill;
    public Weapon ch3weapon1;
    public Weapon ch3weapon2;
    public GameObject ch3sprite;
    public GameObject ch3button;
    public void ch3Join()
    {
        ch3skill.SetActive(true);
        ch3weapon1.gameObject.SetActive(true);
        ch3weapon2.gameObject.SetActive(true);
        ch3sprite.SetActive(true);
        assignWeapons.Add(ch3weapon1);
        assignWeapons.Add(ch3weapon2);
        ch3button.SetActive(true);
    }
    public GameObject ch4skill;
    public Weapon ch4weapon1;
    public Weapon ch4weapon2;
    public GameObject ch4sprite;
    public GameObject ch4button;
    public void ch4Join()
    {
        ch4skill.SetActive(true);
        ch4weapon1.gameObject.SetActive(true);
        ch4weapon2.gameObject.SetActive(true);
        ch4sprite.SetActive(true);
        assignWeapons.Add(ch4weapon1);
        assignWeapons.Add(ch4weapon2);
        ch4button.SetActive(true);
    }
}
