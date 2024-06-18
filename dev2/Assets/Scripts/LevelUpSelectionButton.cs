using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{   //武器的升级和解锁按钮，顺带控制升级和解锁的文本
    public Image CharacterImage;
    public TMP_Text upgradeDescText,nameLevelText,nameLevelLittleText,LevelText;
    public Image weaponIcon;
    public GameObject panel;
    private Weapon assignWeapon;

    public void UpdataButtonDisplay(Weapon theWeapon)
    {
        if(theWeapon.gameObject.activeSelf == true)
        {
            CharacterImage.sprite = theWeapon.stats[theWeapon.weaponLevel + 1].ChIcon;
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel + 1].upgradeText;
        weaponIcon.sprite = theWeapon.icon;
        nameLevelText.text = theWeapon.stats[theWeapon.weaponLevel + 1].nameLevelText;
           nameLevelLittleText.text = theWeapon.stats[theWeapon.weaponLevel + 1].nameLevelLittleText;
            LevelText.text = "LV" + (theWeapon.weaponLevel + 1);
        }
        else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        assignWeapon = theWeapon;
    }
    public void SelectUpgrade()
    {
        if(assignWeapon != null)
        {
            if(assignWeapon.gameObject.activeSelf == true)
            {
                Debug.Log(assignWeapon);
                assignWeapon.LevelUp();
            }else
            {
               // PlayerController.instance.AddWeapon(assignWeapon);
            }
            

            UIController.instance.levelUpPanel.SetActive(false);

            //如果测试组件被启用，则调用测试速度
            if (TestParameter.instance)
            {
                Time.timeScale = 1 + TestParameter.instance.timeScale;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

}
