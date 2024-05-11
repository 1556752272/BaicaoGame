using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject levelUpPanel;
    public Slider explvlSlider;
    public TMP_Text expLvlText;
    public LevelUpSelectionButton[] levelUpButtons;
    public TMP_Text coinText;
    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;
    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;
    public string mainMenuName;
    public GameObject pauseScreen;
    private void Awake()
    {
        instance = this;        
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void UpdateExperience(int currentExp,int levelExp,int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;
        expLvlText.text = "Level: " + currentLvl;
    }
    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
    //这下面的函数都是如果购买函数触发，则跳过升级选项
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minutes + ": " + seconds.ToString("00");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void GoOn()
    {
        pauseScreen.SetActive(false);
        
            Time.timeScale = 1f;
        
    }
    public void Restart()
    {
        pauseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void QuitOut()
    {
        Debug.Log("quit");
        Application.Quit();//打包编译后退出

    }

}
