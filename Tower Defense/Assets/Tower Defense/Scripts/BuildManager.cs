using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    private GameObject selectedTower;

    public GameObject arrowTowerPrefab;
    public GameObject magicTowerPrefab;
    public GameObject canonTowerPrefab;

    private int towerCost;

    public void OnTowerTypeSelect(GameObject towerPrefab)
    {
        selectedTower = towerPrefab;
        towerCost = selectedTower.GetComponent<Tower>().cost;

        BuildTower();
    }

    public void BuildTower()
    {
        if(selectedTower != null)
        {
            if(Managers.Player.coins < towerCost)
            {
                Debug.Log("Not enough money!");
                return;
            }

            Managers.Player.UpdateCoins(-towerCost);

            Instantiate(selectedTower, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }


}
