using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    //[FMODUnity.EventRef]
    //public string buildUISound;
    public GameObject buildCanvas;

    public void OnTowerSpotSelect()
    {
        if(buildCanvas.activeSelf == true)
        {
            buildCanvas.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.buildUI);
        }
        else
        {
            buildCanvas.SetActive(true);
            FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.buildUI);
        }
    }
}
