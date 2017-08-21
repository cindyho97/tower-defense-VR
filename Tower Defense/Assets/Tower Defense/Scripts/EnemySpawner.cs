using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    bool enemySpawned = false;
    public float timeBeforeWave = 10;
    float spawnCDBetweenEnemies = 1f;
    [System.NonSerialized]
    public bool wavesCompleted = false;



    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int nrOfEnemies;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;
	
	// Update is called once per frame
	void Update () {

        
        timeBeforeWave -= Time.deltaTime;
        if(timeBeforeWave < 0)
        {
            enemySpawned = false;
            timeBeforeWave = spawnCDBetweenEnemies;

            SpawnEnemy();
            GetNextWave();
        }
	}

    private void SpawnEnemy()
    {
        // Go through wave comps until we find something to spawn

        foreach (WaveComponent waveComp in waveComps)
        {
            if (waveComp.spawned < waveComp.nrOfEnemies)
            {
                // Spawn enemy
                waveComp.spawned++;
                Instantiate(waveComp.enemyPrefab, transform.position, transform.rotation);
                enemySpawned = true;
                Managers.EnemyMan.enemyCount++;
                break;
            }
        }
    }

    private void GetNextWave()
    {
        if (enemySpawned == false)
        {
            // Wave completed
            Managers.EnemyMan.UpdateNrWaves();
            if (transform.parent.childCount > 1)
            {
                transform.parent.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                // No more enemy waves
                Managers.EnemyMan.wavesCompleted = true;
            }

            Destroy(gameObject);
        }
    }
}
