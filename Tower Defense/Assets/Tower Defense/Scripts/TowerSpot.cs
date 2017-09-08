using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public GameObject buildCanvas;
    FMOD.Studio.EventInstance sfxInstance;

    public void OnTowerSpotSelect()
    {
        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.buildUI);
        sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        
        if (buildCanvas.activeSelf == true)
        {
            buildCanvas.SetActive(false);
            sfxInstance.start();
        }
        else
        {
            buildCanvas.SetActive(true);
            sfxInstance.start();
        }
    }
}
