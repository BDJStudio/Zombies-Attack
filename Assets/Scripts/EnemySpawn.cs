using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    
    public int stepCurrentSpawn;
    public float spawnTime;
    
    public int total;
    public int randomNumber;

    private Vector3 spawnPos;
    private int countEnemyPrefab, timer;
    private float second;
    private DataBase dataBase;

    private void Start()
    {
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();

        for (int i = 0; i < dataBase.mobs.Count; i++)
        {
            total += dataBase.mobs[i].chancePer;
        }

        spawnTime = 2;
        NextSpawn();
    }

    private void Update()
    {
        second += Time.deltaTime;
        if (second >= 1f)
        {
            second = 0;
            timer += 1;
        }
        if (timer >= stepCurrentSpawn)
        {
            timer = 0;
            spawnTime -= 0.1f;
        }
        if(spawnTime <= 0.5f)
        {
            spawnTime = 2;
        }
    }

    public void NextSpawn()
    {
        StartCoroutine(SpawnMotor());
    }

    // выбор стороны спвна мобов
    public void RandomSpawnPoint(int x)
    {
        switch (x)
        {
            case 1:
                spawnPos = new Vector3(Random.Range(-9, 9), 2.4f, Random.Range(-3, -2));
                break;
            case 2:
                spawnPos = new Vector3(Random.Range(9, 10), 2.4f, Random.Range(-9, -2));
                break;
            case 3:
                spawnPos = new Vector3(Random.Range(-10, -9), 2.4f, Random.Range(-9, -2));
                break;
        }
    }

    public void SpawnEnemy()
    {
        int x = Random.Range(1, 4);

        RandomSpawnPoint(x);
        
        Instantiate(enemyPrefab[countEnemyPrefab], spawnPos, Quaternion.identity);
    }

    IEnumerator SpawnMotor()
    {
        ChancePerSpawn();
        SpawnEnemy();
        yield return new WaitForSeconds(spawnTime);
        NextSpawn();
    }

    // функция спавна мобов по их проценту выпадения
    void ChancePerSpawn()
    {
        randomNumber = Random.Range(0, total);

        for(int n = 0; n < dataBase.mobs.Count; n++)
        {
            if (randomNumber <= dataBase.mobs[n].chancePer)
            {
                countEnemyPrefab = n;
                return;
            }
            else
            {
                randomNumber -= dataBase.mobs[n].chancePer;
            }
        }
    }
}
