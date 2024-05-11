using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubble : Weapon
{
    public static PlayerBubble instance;
    public Bubble damager;
    public Transform Spawnplace;
    private float attackCounter, timeBetweenSpawn, direction;
    private float weaponnumber;
    public float shootForce = 3.0f; //泡泡力度应该小一点
    public int killnum = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            attackCounter = timeBetweenSpawn;

            direction = Input.GetAxisRaw("Horizontal");
            for(int i = 0; i < weaponnumber; i++)
            {
                AttackEnemy();//到了时间产生新的泡泡

            }
            SFXManager.instance.PlaySFXPitched(3);


        }

    }

    void SetStats()
    {
        if (weaponLevel == 6)
        {
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        weaponnumber = stats[weaponLevel].amount;
        damager.destroyOnImpactTimes = (int)stats[weaponLevel].acrossEnemyNums;

        attackCounter = 0f;
    }
    public void AttackEnemy()
    {
        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        
        if (enemyTran == Vector3.zero) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);
        Bubble bullet = Instantiate(damager, Spawnplace.position,damager.transform.rotation);
        bullet.gameObject.SetActive(true);
        Vector3 direction =  enemyTran -PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1

            // 为子弹添加速度和方向
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * shootForce;
        
    }
    public void addKillnum()
    {
        if (weaponLevel >= 3)
        { 
            killnum++;
            if (killnum >= 50)
            {
                killnum = 0;
                PlayerHealthController.instance.addHealth(10);

            }

        }
       
    }
}
