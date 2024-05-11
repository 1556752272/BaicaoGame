using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;
    public Transform Spawnplace;
    public float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;
    private float weaponnumber,shotTime;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        shotCounter -=Time.deltaTime;
        if(shotCounter <= 0)
        {
            shotCounter = shotTime;
            //这行代码使用Physics2D.OverlapCircleAll方法检测以当前游戏对象（transform.position）为中心，
            //半径为weaponRange * stats[weaponLevel].range的圆形区域内的所有Collider2D。
            //whatIsEnemy是一个LayerMask，用于指定应该检测哪些层上的敌人。
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            Debug.Log("***********************敌人有:" + enemies.Length);
            if(enemies.Length > 0)
            {
                for(int i = 0; i < weaponnumber; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0,enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    //使用Mathf.Atan2计算从当前游戏对象到目标敌人的弧度角度，然后转换为度数。之后减去90度可能是为了调整投射物的旋转方向。
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    //根据计算出的角度，使用Quaternion.AngleAxis设置投射物的旋转。
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectile, Spawnplace.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

               //  SFXManager.instance.PlaySFXPitched(6);
            }
        }

    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        shotTime = stats[weaponLevel].timeBetweenAttacks;
        shotCounter = 0f;
        weaponnumber = stats[weaponLevel].amount;
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
