using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public BuildManager buildCanvas;
    private CanvasGroup buildCanvasGroup;
    FMOD.Studio.EventInstance sfxInstance;

    private void Start()
    {
        buildCanvasGroup = buildCanvas.GetComponent<CanvasGroup>();
    }
    public void OnTowerSpotSelect()
    {
        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.buildUI);
        sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        
        if (buildCanvasGroup.alpha == 1) // buildCanvas is already active
        {
            buildCanvas.SetBuildCanvas(false);
            sfxInstance.start();
        }
        else
        {
            buildCanvas.playerCoinsText.text = Managers.Player.coins.ToString();
            buildCanvas.SetBuildCanvas(true);
            sfxInstance.start();
        }
    }
}
