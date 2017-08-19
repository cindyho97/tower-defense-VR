using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    private GameObject selectedTower;

    public GameObject[] towerPrefabs;

    public Image[] towerImages;
    private int towerCost;

    public Sprite[] enabledSprites;


    private void Start()
    {
        // Activate tower image if enough money
        int imageIndex = 0;
        foreach(GameObject towerPrefab in towerPrefabs)
        {
            if(Managers.Player.coins >= towerPrefab.GetComponent<Tower>().cost)
            {
                towerImages[imageIndex].sprite = enabledSprites[imageIndex];
                imageIndex++;
            }
        }
    }

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
