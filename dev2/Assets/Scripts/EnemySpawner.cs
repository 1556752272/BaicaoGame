using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;


    public Transform minSpawn, maxSpawn;
    private Transform target;

    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [HideInInspector] public int checkPerFrame;//ÿ�μ����˵�����
    private int enemyToCheck;

    public List<WaveInfo> waves;

    private int currentWave;

    private float waveCounter;

    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
        enemyToCheck = 0;
        currentWave = -1;
        GoToNextWave();
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

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        transform.position = target.position;//����״̬��λ�ã��ڽ�ɫ�߿򸽽�����

        int checkTarget = enemyToCheck + checkPerFrame;

        while(enemyToCheck <checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {//��������Ҿ������despawnDistanceʱɾ����ʧ
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

    public Vector3 SelectSpawnPoint() {//�Զ�������ɵ���λ��
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
            currentWave = 0;//���ò��Σ�������ԶԹ����ǽ�����ǿ
        }

        waveCounter = waves[currentWave].waveLength;//���γ���ʱ��
        spawnCounter = waves[currentWave].timeBetweenSpawns;//����Ƶ��
    }
}

[System.Serializable]

public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}