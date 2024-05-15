using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Weapon
{

    public BulletDamager damager;
    public Transform Spawnplace;
    private float attackCounter, timeBetweenSpawn, direction;
    private float weaponnumber;
    public float shootForce = 5.0f; // 射击力
   // public GameObject bulletPrefab; // 子弹Prefab
    //public Transform bulletSpawnPoint; // 子弹生成点
    private bool addPoisoned;
    private bool ismaxlevel;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        addPoisoned = false;
        damager.poison = false;
        damager.lowbloodKill = false;
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
            if (weaponLevel == 0)
            {
                return;
            }
            //direction = Input.GetAxisRaw("Horizontal");

            //if (direction != 0)//这里就是设置一个生成的时候顺便附带方向
            //{
            //    if (direction > 0)
            //    {
            //        damager.transform.rotation = Quaternion.identity;
            //    }
            //    else
            //    {
            //        damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            //    }
            //}

            //Instantiate(damager, Spawnplace.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            if (weaponLevel >= 2)
            {
                AttackEnemy3nums();
            }
            else
            {
                AttackEnemy();
            }
            
            if (weaponLevel >= 4)
            {
                StartCoroutine(FireSequence());
            }
            if (weaponLevel >= 6)
            {
                if (!ismaxlevel)
                {
                    damager.KillLowBloodEnemy();
                }
            }



        }

    }
    private IEnumerator FireSequence()
    {
        yield return new WaitForSeconds(0.3f); // 等待0.5秒  

        // 发射第二次子弹  
        AttackEnemy3nums();

    }
    void SetStats()
    {
        if (weaponLevel == 3)
        {
            if (addPoisoned == false)
            {
                damager.addPoison();
                addPoisoned = true;
            }

        }
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
        
        Vector3 direction =  enemyTran -PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        //BulletDamager bullet = Instantiate(damager, Spawnplace.position,Quaternion.Euler(new Vector3(0,0,angle)));
        //bullet.gameObject.SetActive(true);
        //    // 为子弹添加速度和方向
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = direction * shootForce;
         InstantiateBullet(Spawnplace.position, angle);
        
    }
    public void AttackEnemy3nums()
    {
        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == Vector3.zero) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = enemyTran - PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        //EnemyDamager bullet = Instantiate(damager, Spawnplace.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        InstantiateBullet(Spawnplace.position, angle);
        float angle2 = angle + 20;
        float angle3 = angle - 20;
        InstantiateBullet(Spawnplace.position, angle2);
        InstantiateBullet(Spawnplace.position, angle3);

    }
    void InstantiateBullet(Vector3 position, float angle)
    {
        BulletDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.gameObject.SetActive(true);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
    }
}
