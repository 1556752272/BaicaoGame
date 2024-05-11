using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{//对金币掉落和添加，花费金币进行更新
    public static CoinController instance;

    public void Awake()
    {
        instance = this;
    }
   
   public int currentCoins;

   public CoinPickup coin;

   public void AddCoins(int coinsToAdd)
   {
        currentCoins += coinsToAdd;
        UIController.instance.UpdateCoins();

       // SFXManager.instance.PlaySFXPitched(2);
   }

   public void DropCoin(Vector3 position, int value)
   {
        //这里可以优化为随机掉落金币以及随机金币的位置
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f,.1f,0f),Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
   }

   public void SpendCoins(int coinToSpend)
   {
     currentCoins -= coinToSpend;

     UIController.instance.UpdateCoins();
   }
}
