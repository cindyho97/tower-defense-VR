using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnSphere : MonoBehaviour, IPointerClickHandler{

    public EnemySpawner enemySpawner;

    public void OnPointerClick(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.spawnEnemy);
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawners").transform.GetChild(0).GetComponent<EnemySpawner>();
        enemySpawner.timeBeforeWave = 0;
        enemySpawner.timeRespawnBar = 0;
    }
}
