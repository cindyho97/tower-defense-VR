using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    float spawnEnemyCD = 0.5f;
    float spawnEnemyCDRemaining = 5;

    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int nrOfEnemies;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        spawnEnemyCDRemaining -= Time.deltaTime;
        if(spawnEnemyCDRemaining < 0)
        {
            bool didSpawn = false;
            spawnEnemyCDRemaining = spawnEnemyCD;

            // Go through wave comps until we find something to spawn

            foreach(WaveComponent waveComp in waveComps)
            {
                if(waveComp.spawned < waveComp.nrOfEnemies)
                {
                    // spawn enemy
                    waveComp.spawned++;
                    Instantiate(waveComp.enemyPrefab, transform.position, transform.rotation);
                    didSpawn = true;
                    break;
                }
            }

            if (didSpawn == false)
            {
                // Wave completed
                // TODO: Instantiate next wave object
                if(transform.parent.childCount > 1)
                {
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // No more enemy waves
                }
                
                Destroy(gameObject);
            }
        }
	}
}
