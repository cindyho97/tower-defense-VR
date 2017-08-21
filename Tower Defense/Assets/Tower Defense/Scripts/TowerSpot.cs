using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public GameObject buildCanvas;

    public void OnTowerSpotSelect()
    {
        if(buildCanvas.activeSelf == true)
        {
            buildCanvas.SetActive(false);
        }
        else
        {
            buildCanvas.SetActive(true);
        }
    }
}
