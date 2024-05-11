using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{   //进行经验获取，掉落和升级
    public static ExperienceLevelController instance;

    void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1,levelCount = 100;//等级上限100

    [HideInInspector]public List<Weapon> weaponsToUpgrade;
    public Levelupend levelUpText;
    
    void Start()
    {
        
    }

    void Update()
    {
        while(expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f ));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel],currentLevel);
    }

    public void SpawnExp(Vector3 position,int expValue)//经验值掉落
    {
       Instantiate(pickup, position, Quaternion.identity,this.transform).expValue = expValue;
    }

    void LevelUp()
    {
        levelUpText.gameObject.SetActive(true);
        
        currentExperience -= expLevels[currentLevel];
        
        currentLevel++;

        if(currentLevel >= expLevels.Count)//防止等级突破上限
        {
            currentLevel = expLevels.Count - 1;
        }
        
        //PlayerController.instance.activeWeapon.LevelUp();

        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //UIController.instance.levelUpButtons[1].UpdataButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.levelUpButtons[0].UpdataButtonDisplay(PlayerController.instance.assignWeapons[0]);

        //UIController.instance.levelUpButtons[1].UpdataButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButtons[2].UpdataButtonDisplay(PlayerController.instance.unassignedWeapons[1]);
        
        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();

        availableWeapons.AddRange(PlayerController.instance.assignWeapons);
        for(int i = 0; i < 3; i++) { 
        if(availableWeapons.Count > 0)//从已经拥有的武器中随机3把武器提供升级
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
        }
        //***********
        //if (PlayerController.instance.assignWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        //{
        //    availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);//把两类武器混合到一起
        //}

        //for (int i = weaponsToUpgrade.Count; i < 3; i++)//这里的3代表有3个升级选项，
        //{
        //    if (availableWeapons.Count > 0)
        //    {
        //        int selected = Random.Range(0, availableWeapons.Count);
        //        weaponsToUpgrade.Add(availableWeapons[selected]);
        //        availableWeapons.RemoveAt(selected);
        //    }
        //}

        for (int i = 0; i < weaponsToUpgrade.Count; i++)//更新这几个按钮的外观文本等等
        {
            UIController.instance.levelUpButtons[i].UpdataButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        //PlayerStatController.instance.UpdateDisplay();//天赋选项
    }
}
