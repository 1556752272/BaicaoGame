using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool poison = false;
    public float maxhealth;
    public float health = 5f;
    
    public int igniteDamage = 5; // 点燃时每半秒掉血值  
    public float igniteInterval = 0.5f; // 点燃状态下掉血的间隔时间（秒）  
    public int deathDamage = 20; // 死亡时对周围敌人造成的伤害  
    public float detectionRadius = 3f; // 检测周围敌人的半径  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public  void TakeDamage(float damageToTake) { }
    virtual public void TakeDamage(float damageToTake, bool shouldKnockback)
    {

    }
    virtual public void TakeDamage(float damageToTake, int fire)
    {

    }
    virtual public void TakeDamage(float damageToTake, bool shouldKnockback,string str)
    {

    }
    virtual public void makeStoned()
    {

    }
    virtual public void makemaxStoned()
    {
        
    }
    virtual public void makeFired(int num)
    {

    }
}
