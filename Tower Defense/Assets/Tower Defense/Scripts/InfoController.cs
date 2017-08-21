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


    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.WAVE_UPDATED, OnWaveUpdated);
    }

    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.WAVE_UPDATED, OnWaveUpdated);
    }

    private void OnHealthUpdated()
    {
        healthText.text = Managers.Player.health.ToString();
    }

    private void OnCoinsUpdated()
    {
        coinsText.text = Managers.Player.coins.ToString();
    }

    private void OnWaveUpdated()
    {
        waveText.text = Managers.EnemyMan.nrWaves.ToString();
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        // TODO: show level complete screen
        Debug.Log("Level Completed!");
        yield return new WaitForSeconds(0);
        // TODO: go to next level
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel()
    {
        // TODO: show level failed screen
        Debug.Log("Level Failed!");
        yield return new WaitForSeconds(0);

        Managers.Player.Respawn();
        // Manager.Level.RestartLevel
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
