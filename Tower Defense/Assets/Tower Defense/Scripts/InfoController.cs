using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoController : MonoBehaviour {

    public Text healthText;
    public Text maxHealthText;
    public Text coinsText;
    public Text waveText;

    private int nrOfBuildCanvasses = 6;
    private BuildManager[] buildCanvasses;

    private void Awake()
    { 
        OnCoinsUpdated();
        
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.AddListener(GameEvent.WAVE_UPDATED, OnWaveUpdated);
    }

    private void Start()
    {
        buildCanvasses = new BuildManager[nrOfBuildCanvasses];
        FindBuildCanvasses();
    }

    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.AddListener(GameEvent.WAVE_UPDATED, OnWaveUpdated);
    }

    private void OnHealthUpdated()
    {
        healthText.text = Managers.Player.health.ToString();
    }

    private void OnCoinsUpdated()
    {
        coinsText.text = Managers.Player.coins.ToString();
        if(buildCanvasses != null)
        {
            foreach (BuildManager buildCanvas in buildCanvasses)
            {
                buildCanvas.UpdateBuildUI();
            }
        }
    }

    private void OnWaveUpdated()
    {
        waveText.text = Managers.EnemyMan.nrWaves.ToString();
    }

    

    private void FindBuildCanvasses()
    {
        GameObject[] buildCanvasObjs = new GameObject[nrOfBuildCanvasses];
        buildCanvasObjs = GameObject.FindGameObjectsWithTag("BuildManager");

        for (int i = 0; i < buildCanvasObjs.Length; i++)
        {
            buildCanvasses[i] = buildCanvasObjs[i].GetComponent<BuildManager>();
        }
    }
}
