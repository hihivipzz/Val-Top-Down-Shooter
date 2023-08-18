using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    public GameObject boss;
    public Transform bossSpawnPoint;

    public GameObject healthBar;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finished;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaveIndex = 0;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for(int i=0;i<currentWave.count;i++)
        {
            if(player==null)
            {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

            if(i==currentWave.count-1)
            {
                finished= true;
            }
            else
            {
                finished = false; 
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(finished==true && GameObject.FindGameObjectsWithTag("Enemy").Length==0) 
        {
            finished= false;
            if(currentWaveIndex +1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(SpawnWave(currentWaveIndex));
            }
            else
            {
                Instantiate(boss,bossSpawnPoint.position,bossSpawnPoint.rotation);
                healthBar.SetActive(true);
            }
        }
    }
}
