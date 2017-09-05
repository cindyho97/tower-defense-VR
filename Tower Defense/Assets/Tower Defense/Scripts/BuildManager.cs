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

    public Text playerCoinsText;
    public Text arrowCostText;
    public Text magicCostText;
    public Text canonCostText;

    public GameObject buildTimeCanvas;
    public Image buildTimeBar;
    
    public bool towerBuild = false;
    private int arrowCost;
    private int magicCost;
    private int canonCost;

    private GameObject previousTower;
    private Color red = new Color(255, 0, 0, 200);
    private Color white = new Color(255, 255, 255);
    private Color gray = new Color32(130, 130, 130,255);
    private Image currentImage;
    private Vector3 buildSignPosition;
    private Quaternion buildSignRotation;

    private bool buildingNow = false;
    private float buildTimeRemaining;
    private int buildStartTime = 3;

    private void Start()
    {
        arrowCost = arrowPrefab.GetComponent<Tower>().cost;
        magicCost = magicPrefab.GetComponent<Tower>().cost;
        canonCost = canonPrefab.GetComponent<Tower>().cost;

        playerCoinsText.text = Managers.Player.coins.ToString();
        arrowCostText.text = arrowCost.ToString();
        magicCostText.text = magicCost.ToString();
        canonCostText.text = canonCost.ToString();

        ChangeEnableColor();
        buildTimeRemaining = buildStartTime;
    }

    // Give existing towers a special color
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
        arrowImage.color = (Managers.Player.coins >= arrowCost) ? white : gray;
        magicImage.color = (Managers.Player.coins >= magicCost) ? white : gray;
        canonImage.color = (Managers.Player.coins >= canonCost) ? white : gray;
    }

    public void OnTowerTypeSelect(GameObject towerPrefab)
    {
        selectedTower = towerPrefab;

        if(previousTower != null)
        {
            towerBuild = true;
        }

        CheckTowerAvailable();
        
    }

    private void CheckTowerAvailable()
    {
        if (Managers.Player.coins < selectedTower.GetComponent<Tower>().cost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        else if(previousTower == selectedTower)
        {
            Debug.Log("Can't buy the same tower.");
            return;
        }

        int towerCost = selectedTower.GetComponent<Tower>().cost;
        Managers.Player.UpdateCoins(-towerCost);
        playerCoinsText.text = Managers.Player.coins.ToString();
        BuildTower();
    }

    public void BuildTower()
    {
        if(selectedTower != null)
        {
            
            if (towerBuild) // Tower already exists
            {
                currentImage.color = white;
                GameObject currentTower = transform.parent.gameObject.GetComponentInChildren<Tower>().gameObject;
                Destroy(currentTower);
            }
            else
            {
                GameObject buildSign = transform.parent.Find("BuildSign").gameObject;
                buildSignPosition = buildSign.transform.position;
                buildSignRotation = buildSign.transform.rotation;
                Destroy(buildSign);
                towerBuild = true;
            }

            SetBuildCanvas(false);

            StartCoroutine(WaitForBuiltTime());
            
        }
    }

    private IEnumerator WaitForBuiltTime()
    {
        buildTimeCanvas.SetActive(true);
        buildingNow = true;

        while(buildTimeRemaining > 0)
        {
            buildTimeRemaining -= Time.deltaTime;
            buildTimeBar.fillAmount = buildTimeRemaining / buildStartTime;
            yield return null;
            
        }

        buildTimeRemaining = buildStartTime;
        InstantiateTower();
    }

    private void InstantiateTower()
    {
        Instantiate(selectedTower, buildSignPosition, buildSignRotation, transform.parent);
        previousTower = selectedTower;
        SetBuildCanvas(false);
    }

    public void SetBuildCanvas(bool enableCanvas)
    {
        CanvasGroup buildCanvasGroup = GetComponent<CanvasGroup>();
        if (enableCanvas)
        {
            buildCanvasGroup.alpha = 1;
            buildCanvasGroup.interactable = true;
            buildCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            buildCanvasGroup.alpha = 0;
            buildCanvasGroup.interactable = false;
            buildCanvasGroup.blocksRaycasts = false;
        }   
    }

    


}
