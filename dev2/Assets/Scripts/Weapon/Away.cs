using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Away : Weapon
{
    
    public Transform Spawnplace;
    
    public AwayDamagers damager;


    void Start()
    {
        SetStats();
    }


    void Update()
    {
           //Instantiate(damager,Spawnplace.position, Quaternion.identity,this.transform).gameObject.SetActive(true);
    }

    

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.attackTime = stats[weaponLevel].speed;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        //timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        damager.lifeTime = stats[weaponLevel].duration;
        //weaponnumber = stats[weaponLevel].amount;
        //spawnCounter = 0f;
    }
    public void changeDamager()
    {
        Instantiate(damager, Spawnplace.position, Quaternion.identity, this.transform).gameObject.SetActive(true);
    }
}
