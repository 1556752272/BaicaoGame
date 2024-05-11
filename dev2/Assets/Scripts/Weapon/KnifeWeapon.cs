using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeWeapon : Weapon
{
    public KnifeDamager damager;
    public Transform Spawnplace;
    private float spawnCounter, timeBetweenSpawn, direction;//这里应该是attackTime，但是很奇怪
    private float weaponnumber;
    private int attackTimer=0;
    public EnemyDamager SwordWind;
    public float shootForce = 5.0f; // 射击力
    public CharacterAnimation mainch;
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

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            SFXManager.instance.PlaySFXPitched(7);
           if (mainch.face_right)
           {
                
                Instantiate(damager, Spawnplace.position + new Vector3(1f, 0, 0), Quaternion.Euler(0f, 0f,0f), transform).gameObject.SetActive(true);
            }
            else
            {
                Instantiate(damager, Spawnplace.position + new Vector3(-1f, 0, 0), Quaternion.Euler(0f, 0f, 180f), transform).gameObject.SetActive(true);
            }
            

            //Instantiate(damager, Spawnplace.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            if (weaponLevel >= 6)
            {
                if (mainch.face_right)
                {
                    Instantiate(damager, Spawnplace.position + new Vector3(2f, 0, 0), Quaternion.Euler(0f, 0f, 30f), transform).gameObject.SetActive(true);
                }
                else
                {
                    Instantiate(damager, Spawnplace.position + new Vector3(-2f, 0, 0), Quaternion.Euler(0f, 0f, 220f), transform).gameObject.SetActive(true);
                }
            }
            if (weaponLevel >= 3)
            {
                attackTimer++;
                if (attackTimer == 4)
                {
                    attackTimer = 0;
                    AttackEnemy();//发射剑气
                }
            }
            if (weaponLevel >= 5)
            {
                //if (direction > 0)
                //{
                //    //damager.transform.rotation = Quaternion.identity;
                //    Instantiate(damager, Spawnplace.position, Quaternion.Euler(0f, 0f, 180f), transform).gameObject.SetActive(true);
                //    if (weaponLevel >= 6)
                //    {
                //        Instantiate(damager, Spawnplace.position, Quaternion.Euler(0f, 0f, 230f), transform).gameObject.SetActive(true);
                //    }
                //}
                //else
                //{
                //    //damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                //    Instantiate(damager, Spawnplace.position, Quaternion.identity, transform).gameObject.SetActive(true);
                //    if (weaponLevel >= 6)
                //    {
                //        Instantiate(damager, Spawnplace.position, damager.transform.rotation * Quaternion.Euler(0f, 0f, 20f), transform).gameObject.SetActive(true);
                //        Instantiate(damager, Spawnplace.position, Quaternion.Euler(0f, 0f, 60f), transform).gameObject.SetActive(true);
                //    }
                //}
                if (mainch.face_right)
                {
                    Instantiate(damager, Spawnplace.position+new Vector3(-2f, 0,0), Quaternion.Euler(0f, 0f, 180f), transform).gameObject.SetActive(true);
                }
                else
                {
                    Instantiate(damager, Spawnplace.position + new Vector3(2f, 0, 0), Quaternion.Euler(0f, 0f, 0f), transform).gameObject.SetActive(true);
                }


                
                if (weaponLevel >= 6)
                {
                    if (mainch.face_right)
                    {
                        Instantiate(damager, Spawnplace.position + new Vector3(-2f, 0, 0), Quaternion.Euler(0f, 0f, 210f), transform).gameObject.SetActive(true);
                    }
                    else
                    {
                        Instantiate(damager, Spawnplace.position + new Vector3(2f, 0, 0), Quaternion.Euler(0f, 0f,30f), transform).gameObject.SetActive(true);
                    }
                }
            }


        }

    }

    void SetStats()
    {
        if (weaponLevel == 6)
        {
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.attackTime = stats[weaponLevel].speed;
        damager.damageAmount = stats[weaponLevel].damage;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        weaponnumber = stats[weaponLevel].amount;
        damager.lifeTime = stats[weaponLevel].duration;
        spawnCounter = 0f;
    }
    public void AttackEnemy()
    {
        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == null) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = enemyTran - PlayerController.instance.transform.position;
        direction.Normalize(); // 确保方向向量的长度为1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        EnemyDamager bullet = Instantiate(SwordWind, Spawnplace.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        bullet.gameObject.SetActive(true);
        // 为子弹添加速度和方向
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * shootForce;//这里可以调整力度

    }

}
