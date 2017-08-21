using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }

    public int nrWaves { get; private set; }
    public int enemyCount;
    public bool wavesCompleted;


    public void Startup()
    {
        UpdateData(0, 0);

        status = ManagerStatus.Started;
    }

    private void Update()
    {
        CheckNrOfEnemies();
        Debug.Log("Enemy count: " + enemyCount);
    }

    void UpdateData(int enemyCount, int nrWaves)
    {
        this.enemyCount = enemyCount;
        this.nrWaves = nrWaves;
    }

    public void UpdateNrWaves()
    {
        nrWaves++;
        Messenger.Broadcast(GameEvent.WAVE_UPDATED);  
    }

    public void CheckNrOfEnemies()
    {
        if (wavesCompleted && enemyCount == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
        }
    }

}
