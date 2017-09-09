using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    
    private float startTimeRespawnBar;
    private bool enemySpawned = false;
    private Image respawnTimerBar;
    private float spawnCDBetweenEnemies = 1f;
    private bool soundPlayed = false;
    FMOD.Studio.EventInstance sfxInstance;

    [System.NonSerialized]
    public bool wavesCompleted = false;
    [System.NonSerialized]
    public float timeRespawnBar;

    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int nrOfEnemies;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;
    public float timeBeforeWave = 10;

    private void Start()
    {
        respawnTimerBar = GameObject.FindGameObjectWithTag("RespawnBar").GetComponent<Image>();
        startTimeRespawnBar = timeBeforeWave;
        timeRespawnBar = timeBeforeWave;
    }
    // Update is called once per frame
    void Update () {

        respawnTimerBar.fillAmount = timeRespawnBar / startTimeRespawnBar;

        timeBeforeWave -= Time.deltaTime;
        timeRespawnBar -= Time.deltaTime;


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
        if (!soundPlayed)
        {
            sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.spawnEnemy);
            sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            sfxInstance.start();
        }
        soundPlayed = true;

        // Go through wave comps until we find something to spawn
        foreach (WaveComponent waveComp in waveComps)
        {
            if (waveComp.spawned < waveComp.nrOfEnemies)
            {
                // Spawn enemy
                waveComp.spawned++;
                Instantiate(waveComp.enemyPrefab, transform.parent.position, transform.rotation);
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
            soundPlayed = false;
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
