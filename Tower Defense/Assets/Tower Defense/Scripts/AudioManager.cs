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
    public string towerBuild;
    [FMODUnity.EventRef]
    public string winMusic;
    [FMODUnity.EventRef]
    public string loseMusic;
    [FMODUnity.EventRef]
    public string bgMusic;

    public FMOD.Studio.EventInstance sfxInstance;

    public void Startup()
    {
        status = ManagerStatus.Started;
    }

    private void Start()
    {
        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.bgMusic);
        sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        sfxInstance.start();
    }

    private void OnDestroy()
    {
        sfxInstance.release();
    }
}
