using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour {

    public Text endText;
    public Transform fireworkParent;
    private CanvasGroup endCanvas;
    FMOD.Studio.EventInstance endMusic;

    private Light[] lights = new Light[4];

    private void Awake()
    {
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        endCanvas = GetComponent<CanvasGroup>();
        GameObject[] lightObjs = GameObject.FindGameObjectsWithTag("AllSidesLight");
        
        for (int i = 0; i < lightObjs.Length; i++)
        {
            lights[i] = lightObjs[i].GetComponent<Light>();
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
    }

    public void OnLevelComplete()
    {
        
        endMusic = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.winMusic);
        endMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        endMusic.start();

        endText.text = "Congratulations!\nYou won!";
        SetEndCanvas(true);
        StartCoroutine(StartFireworks());
    }

    public void OnLevelFailed()
    {
        StartCoroutine(DimLights());

        endMusic = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.loseMusic);
        endMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        endMusic.start();

        endText.text = "Awwww you lost :(\nBetter next time!";
        SetEndCanvas(true);
    }


    public void OnRestartButton()
    {
        endMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SetEndCanvas(false);
        PauseGame(false);

        Managers.Player.Respawn();
        endMusic.release();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButton()
    {
        endMusic.release();
        Application.Quit();
    }

    private void SetEndCanvas(bool enableCanvas)
    {
        if (enableCanvas)
        {
            endCanvas.alpha = 1;
            endCanvas.interactable = true;
            endCanvas.blocksRaycasts = true;
            Managers.AudioMan.sfxInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            TransportPlayer();
            PauseGame(true);
        }
        else
        {
            endCanvas.alpha = 0;
            endCanvas.interactable = false;
            endCanvas.blocksRaycasts = false;
        }
    }

    private void PauseGame(bool isPaused)
    {
        Time.timeScale = (isPaused) ? 0 : 1;
    }

    private void TransportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 5, -3);
    }

    private IEnumerator DimLights()
    {
        float elapsed = 0f;
        float duration = 5f;
        float minInt = 0.25f;
        float maxInt = 0.7f;
        float lightValue;

        while (elapsed < duration)
        {
            lightValue = Mathf.Lerp(maxInt, minInt, elapsed / duration);
            foreach(Light light in lights)
            {
                light.intensity = lightValue;
            }
            elapsed += .025f; // Don't use Time.deltaTime --> Time.timescale = 0
            yield return null;
        }
    }

    private IEnumerator StartFireworks()
    {
        for(int i = 0; i < fireworkParent.childCount; i++)
        {
            fireworkParent.GetChild(i).gameObject.SetActive(true);
        }
        yield return null;
    }
}
