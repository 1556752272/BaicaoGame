using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;
    [HideInInspector]
    public bool statsUpdated;
    public Sprite icon;
    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            Debug.Log(weaponLevel);
            weaponLevel++;
            statsUpdated = true;
        }
    }
}
[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration,acrossEnemyNums,slowEffects;
    public Sprite ChIcon;
             //伤害间隔，伤害，武器大小，武器再生时间，武器数量，持续时间 ，穿透敌人数量，击退效果，减速敌人效果
    public string upgradeText;
    public string nameLevelText;
    public string nameLevelLittleText;
}
