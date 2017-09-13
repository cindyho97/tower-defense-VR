using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }

    public int nrWaves { get; private set; }
    public int enemyCount;
    public bool wavesCompleted;
    private int lastEnemyCount;
    private bool levelCompleted = false;


    public void Startup()
    {
        UpdateData(0);

        status = ManagerStatus.Started;
    }

    private void Update()
    {
        if (wavesCompleted)
        {
            CheckNrOfEnemies();
        }
        
    }

    void UpdateData( int nrWaves)
    {
        this.nrWaves = nrWaves;
    }

    public void UpdateNrWaves()
    {
        nrWaves++;
        Messenger.Broadcast(GameEvent.WAVE_UPDATED);  
    }

    public void CheckNrOfEnemies()
    {
        lastEnemyCount = GameObject.FindObjectsOfType<Enemy>().Length;

        if(lastEnemyCount <= 0 && levelCompleted == false)
        {
            levelCompleted = true;
            Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
        }
    }

}
