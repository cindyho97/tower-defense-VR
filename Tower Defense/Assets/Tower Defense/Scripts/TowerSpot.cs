using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public Canvas buildCanvas;

    public void OnTowerSpotSelect()
    {
        Debug.Log("Tower spot clicked");

        buildCanvas.gameObject.SetActive(true);
    }
}
