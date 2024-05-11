using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfireball : Weapon
{

    public FireDamager damager;
    public Transform Spawnplace;
    private float attackCounter, timeBetweenSpawn, direction;
    private float weaponnumber;
    public float shootForce = 4.0f; // 射击力
    void Start()
    {

        shootForce = 4f;
        damager.firenumber =2;
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


            AttackEnemy();

            SFXManager.instance.PlaySFXPitched(5);

        }

    }

    void SetStats()
    {
        if (weaponLevel == 1)
        {
            damager.firenumber = 5;
        }
        if (weaponLevel == 4)
        {
            damager.firenumber = 10;
        }
        
        if (weaponLevel == 5)
        {
            shootForce = 7f;
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
        //EnemyDamager bullet = Instantiate(damager, Spawnplace.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        InstantiateBullet(Spawnplace.position, angle);
        if (weaponLevel >= 2)
        {
            float angle2 = angle+20;
            float angle3 = angle - 20;
            InstantiateBullet(Spawnplace.position, angle2);
            InstantiateBullet(Spawnplace.position, angle3);
            if (weaponLevel >= 5)
            {
                float angle4 = angle + 40;
                float angle5 = angle - 40;
                InstantiateBullet(Spawnplace.position, angle4);
                InstantiateBullet(Spawnplace.position, angle5);

            }
        }
        

    }
    void InstantiateBullet(Vector3 position,float angle)
    {
        FireDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.gameObject.SetActive(true);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad)* shootForce,Mathf.Sin(angle*Mathf.Deg2Rad)* shootForce) ;
    }
}
