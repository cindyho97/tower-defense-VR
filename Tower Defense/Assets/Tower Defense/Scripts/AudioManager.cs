using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager{

    public ManagerStatus status { get; private set; }

    [FMODUnity.EventRef]
    public string teleport;
    [FMODUnity.EventRef]
    public string buildUI;
    [FMODUnity.EventRef]
    public string canonExplosion;
    [FMODUnity.EventRef]
    public string spawnEnemy;
    [FMODUnity.EventRef]
    public string buildTimeBar;
    [FMODUnity.EventRef]
    public string magicBall;
    [FMODUnity.EventRef]
    public string arrowDamage;
    [FMODUnity.EventRef]
    public string coinDrop;
    

    public void Startup()
    {
        status = ManagerStatus.Started;
    }
}
