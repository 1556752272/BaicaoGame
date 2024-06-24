using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//测试用刷怪方案
public class EnemySpawn2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    void Start()
    {
        StartCoroutine(ISpawnEnemy());//刷怪协程
    }

    
    void Update()
    {
        
    }

    //刷怪协程
    IEnumerator ISpawnEnemy()
    {
        while (true)
        {
            //玩家位置
            Vector3 pos = PlayerController.instance.transform.position;
            //随机方向
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            //Debug.Log(random_pos);
            //怪物生成位置
            pos = pos + randomDirection * 10;
            // 实例化对象
            Instantiate(enemyPrefab, pos, Quaternion.identity);

            // 等待1秒
            yield return new WaitForSeconds(1);
        }
    }

}
