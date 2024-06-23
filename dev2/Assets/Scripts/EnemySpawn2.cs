using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ˢ�ַ���
public class EnemySpawn2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    void Start()
    {
        StartCoroutine(ISpawnEnemy());//ˢ��Э��
    }

    
    void Update()
    {
        
    }

    //ˢ��Э��
    IEnumerator ISpawnEnemy()
    {
        while (true)
        {
            //���λ��
            Vector3 pos = PlayerController.instance.transform.position;
            //�������
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            //Debug.Log(random_pos);
            //��������λ��
            pos = pos + randomDirection * 10;
            // ʵ��������
            Instantiate(enemyPrefab, pos, Quaternion.identity);

            // �ȴ�1��
            yield return new WaitForSeconds(1);
        }
    }

}
