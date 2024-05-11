using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatbackWeapon : Weapon
{

    public StoneDamager damager;
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
        damager.makeEnemyStone = false;
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
            Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
            if (enemyTran == null) return;
            AttackEnemy();
            SFXManager.instance.PlaySFXPitched(6);

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
        if (weaponLevel == 5)
        {
            damager.makeStone();
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

        Vector3 direction = enemyTran - PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        StoneDamager bullet = Instantiate(damager, Spawnplace.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        bullet.gameObject.SetActive(true);
        // 为子弹添加速度和方向
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * shootForce;
        if (weaponLevel >= 2)
        {
            Vector3 direction2 = enemyTran - PlayerController.instance.transform.position;
            ShootBullet(direction2);
        }
        if (weaponLevel >= 4)
        {
            ShootBullet2(direction);
        }

    }
    private void ShootBullet(Vector3 direction2)
    {
        // 实例化子弹  
        //EnemyDamager bullet = Instantiate(damager, transform.position, Quaternion.identity);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == Vector3.zero) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = direction2;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        //EnemyDamager bullet = Instantiate(damager, Spawnplace.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        InstantiateBullet(Spawnplace.position, angle+180);
        
        //float angle2 = angle + 20;
        //float angle3 = angle - 20;
        //InstantiateBullet(Spawnplace.position, angle2);
        //InstantiateBullet(Spawnplace.position, angle3);
    }
    private void ShootBullet2(Vector3 direction2)
    {
        // 实例化子弹  
        //EnemyDamager bullet = Instantiate(damager, transform.position, Quaternion.identity);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == Vector3.zero) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = direction2;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        
        float angle2 = angle + 90;
        float angle3 = angle - 90;
        InstantiateBullet(Spawnplace.position, angle2);
        InstantiateBullet(Spawnplace.position, angle3);
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
        StoneDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        bullet.gameObject.SetActive(true);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
    }

}
