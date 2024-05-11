using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager damager;
    public Transform Spawnplace;
    private float spawnTime,spawnCounter;//武器再次生成的计时

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {   //如果收到statsUpdated == true则重新更新武器状态，每次到达spawntime,则重新生成武器EnemyDamager damager;
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;
            Instantiate(damager, transform).gameObject.SetActive(true);
            
            //Instantiate(damager, new Vector3(0, 0, 0), Quaternion.identity,transform).gameObject.SetActive(true);

             //SFXManager.instance.PlaySFXPitched(10);
        }
    }

    void SetStats()
    {
        //damage伤害 duration持续时间 speed = timeBetweenDamage，localScale = range当前武器大小比例，
        //    spawntime = timebetweenAttacks每次武器生成的时间
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.attackTime = stats[weaponLevel].speed;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        spawnTime = stats[weaponLevel].timeBetweenAttacks;

        spawnCounter = 0f;

    }
}
