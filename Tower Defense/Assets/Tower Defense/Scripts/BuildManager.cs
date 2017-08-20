using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    private GameObject selectedTower;

    public GameObject arrowPrefab;
    public GameObject magicPrefab;
    public GameObject canonPrefab;

    public Image arrowImage;
    public Image magicImage;
    public Image canonImage;
    
    public bool towerBuild = false;

    private GameObject previousTower;
    private Color red = new Color(255, 0, 0, 200);
    private Color white = new Color(255, 255, 255);
    private Image currentImage;
    private Vector3 platformPosition;
    private Quaternion platformRotation;

    private void Start()
    {
        ChangeEnableColor();     
    }

    // Give tower already build a disable color
    public void ChangeTowerBuildColor()
    {
        if(previousTower == arrowPrefab)
        {
            currentImage = arrowImage;
        }
        else if(previousTower == magicPrefab)
        {
            currentImage = magicImage;
        }
        else if(previousTower == canonPrefab)
        {
            currentImage = canonImage;
        }
        currentImage.color = red;
    }


    public void ChangeEnableColor()
    {
        if(Managers.Player.coins >= arrowPrefab.GetComponent<Tower>().cost)
        {
            arrowImage.color = white;
        }
        
        if(Managers.Player.coins >= magicPrefab.GetComponent<Tower>().cost)
        {
            magicImage.color = white; ;
        }

        if(Managers.Player.coins >= canonPrefab.GetComponent<Tower>().cost)
        {
            canonImage.color = white;
        }
    }

    public void OnTowerTypeSelect(GameObject towerPrefab)
    {
        selectedTower = towerPrefab;
        if(previousTower != null)
        {
            towerBuild = true;
        }
        BuildTower();
    }

    public void BuildTower()
    {
        if(selectedTower != null)
        {
            if (Managers.Player.coins < selectedTower.GetComponent<Tower>().cost || previousTower == selectedTower)
            {
                Debug.Log("Not enough money or same tower");
                return;
            }

            int towerCost = selectedTower.GetComponent<Tower>().cost;
            Managers.Player.UpdateCoins(-towerCost);

            if (towerBuild)
            {
                currentImage.color = white;
                GameObject currentTower = transform.parent.gameObject.GetComponentInChildren<Tower>().gameObject;
                Destroy(currentTower);
            }
            else
            {
                GameObject platform = transform.parent.Find("Platform").gameObject;

                platformPosition = platform.transform.position;
                platformRotation = platform.transform.rotation;
                Destroy(platform);
                towerBuild = true;
            }

            Instantiate(selectedTower, platformPosition, platformRotation, transform.parent);
            previousTower = selectedTower;
            gameObject.SetActive(false);
        }
    }


}
