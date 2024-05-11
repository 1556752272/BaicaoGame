using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
    public Transform Spawnplace;
    public Transform holder,fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    private float weaponnumber;
    public EnemyDamager damager;

    
    void Start()
    {
        SetStats();

       // UIController.instance.levelUpButtons[0].UpdataButtonDisplay(this);
    }

  
    void Update()
    {
        //holder.rotation = Quaternion.Euler(0f,0f,holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime* damager.timeBetweenDamage));
        //fireballToSpawn.rotation = Quaternion.Euler(0f, 0f, fireballToSpawn.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        holder.rotation = Quaternion.Euler(0f,0f,holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime ));
        
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation,holder).gameObject.SetActive(true);

            for (int i = 0; i < weaponnumber; i++)
            {
                float rot = (360f / weaponnumber) * i;
                
                Instantiate(fireballToSpawn, fireballToSpawn.position+Spawnplace.position, Quaternion.Euler(0f, 0f, rot), holder).
                    gameObject.SetActive(true);
            }

           // SFXManager.instance.PlaySFXPitched(8);
        }

        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.attackTime = stats[weaponLevel].speed;
        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;
        weaponnumber = stats[weaponLevel].amount;
        spawnCounter = 0f;
    }
}
