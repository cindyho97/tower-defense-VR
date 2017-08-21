using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour {

    public EnemySpawner enemySpawner;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSpawnSphere()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawners").transform.GetChild(0).GetComponent<EnemySpawner>();
        enemySpawner.timeBeforeWave = 0;
    }
}
