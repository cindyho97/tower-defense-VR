﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public Canvas buildCanvas;

    public void OnTowerSpotSelect()
    {
        Debug.Log("Tower spot clicked");

        buildCanvas.gameObject.SetActive(true);
    }

    /*
    public void BuildTower()
    {
        BuildManager bm = GameObject.FindObjectOfType<BuildManager>();
        if (bm.selectedTower != null)
        {
            if (Managers.Player.coins < bm.selectedTower.GetComponent<Tower>().cost)
            {
                Debug.Log("Not enough money");
                return;
            }

            Managers.Player.UpdateCoins(-bm.selectedTower.GetComponent<Tower>().cost);

            // FIXME: we assume that we are an object nested in a parent
            Instantiate(bm.selectedTower, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }
    */
}
