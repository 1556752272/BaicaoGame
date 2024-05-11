using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{
    public Transform Spawnplace;
    public EnemyDamager damager;
    private float spawnTime;
    private float throwCounter;
    private float weaponnumber;
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

        throwCounter -= Time.deltaTime;
        if(throwCounter <= 0)
        {
            throwCounter = spawnTime;

            for(int i = 0; i <  weaponnumber; i++)
            {
                Instantiate(damager, Spawnplace.position, damager.transform.rotation).gameObject.SetActive(true);
            }

         //   SFXManager.instance.PlaySFXPitched(4);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        spawnTime = stats[weaponLevel].timeBetweenAttacks;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        weaponnumber = stats[weaponLevel].amount;
        throwCounter = 0f;
    }
}
