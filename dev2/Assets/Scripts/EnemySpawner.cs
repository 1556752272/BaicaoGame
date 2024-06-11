using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemyToSpawn;

    //public float timeToSpawn;
    private float spawnCounter;
    private float waveCounter;
    private float spawnCounter2;
    private float waveCounter2;

    public Transform minSpawn, maxSpawn;
    private Transform target;

    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [HideInInspector] public int checkPerFrame;//每次检查敌人的数量
    private int enemyToCheck;

    public List<WaveInfo> waves;
    public List<WaveInfo> waves2;
    private int currentWave;
    private int currentWave2;
    

    void Start()
    {
        //spawnCounter = timeToSpawn;
        checkPerFrame = 10;//在这里添加一行代码，每帧数检查10名敌人
        target = PlayerHealthController.instance.transform;

        despawnDistance = 20;//这里是敌人距离玩家多远就会开始消失的数字
        enemyToCheck = 0;
        currentWave = -1;
        currentWave2 = -1;
        GoToNextWave();
        GoToNextWave2();
    }

    void Update()
    {
        /*spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0) {
            spawnCounter = timeToSpawn;
            //Instantiate(enemyToSpawn, transform.position, transform.rotation);

            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }*/

        if(PlayerHealthController.instance.gameObject.activeSelf)
        {
            if(currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if(waveCounter <= 0)
                {
                    GoToNextWave();
                }

                spawnCounter -= Time.deltaTime;
                if(spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;
                    if (waves[currentWave].enemyToSpawn != null)
                    {
                        GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                        Debug.Log("产生");
                        spawnedEnemies.Add(newEnemy);

                    }
                    
                }
            }
            if (currentWave2 < waves2.Count)
            {
                waveCounter2 -= Time.deltaTime;
                if (waveCounter2 <= 0)
                {
                    GoToNextWave2();
                }

                spawnCounter2 -= Time.deltaTime;
                if (spawnCounter2 <= 0)
                {
                    spawnCounter2 = waves2[currentWave2].timeBetweenSpawns;
                    if (waves2[currentWave2].enemyToSpawn != null)
                    {
                        GameObject newEnemy2 = Instantiate(waves2[currentWave2].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                        spawnedEnemies.Add(newEnemy2);
                    }
                }
            }
        }

        transform.position = target.position;//锁定状态机位置，在角色边框附近生成

        int checkTarget = enemyToCheck + checkPerFrame;

        while(enemyToCheck <checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(PlayerController.instance.transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)//这里也要修改
                    {//敌人离玩家距离大于despawnDistance时删除消失
                        Destroy(spawnedEnemies[enemyToCheck]);

                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint() {//自动随机生成敌人位置
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = UnityEngine.Random.Range(0f, 1f) > .5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = UnityEngine.Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else {
            spawnPoint.x = UnityEngine.Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }

        }

        return spawnPoint;
    }
    public void GoToNextWave()
    {
        currentWave++;
        
        if(currentWave >= waves.Count)
        {
            currentWave = 0;//重置波次，这里可以对怪物们进行增强
        }

        waveCounter = waves[currentWave].waveLength;//波次持续时间
        spawnCounter = waves[currentWave].timeBetweenSpawns;//生成频率
    }
    public void GoToNextWave2()
    {
        currentWave2++;

        if (currentWave2 >= waves2.Count)
        {
            currentWave2 = 0;//重置波次，这里可以对怪物们进行增强
        }

        waveCounter2 = waves2[currentWave2].waveLength;//波次持续时间
        spawnCounter2 = waves2[currentWave2].timeBetweenSpawns;//生成频率
    }
}

[System.Serializable]

public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}