using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    private GameObject selectedTower;

    public GameObject arrowPrefab;
    public GameObject magicPrefab;
    public GameObject canonPrefab;

    public Button arrowButton;
    public Button magicButton;
    public Button canonButton;

    public Text playerCoinsText;
    public Text arrowCostText;
    public Text magicCostText;
    public Text canonCostText;

    public GameObject buildTimeCanvas;
    public Image buildTimeBar;
    public GameObject halfTowerSpot;
    
    public bool towerBuild = false;
    private int arrowCost;
    private int magicCost;
    private int canonCost;

    private GameObject previousTower;
    private Color red = new Color(255, 0, 0, 200);
    private Color white = new Color(255, 255, 255);
    private Color gray = new Color32(130, 130, 130, 255);
    private Color yellow = new Color32(244, 225, 66, 255);
    private Image currentImage;
    private Vector3 buildSignPosition;
    private Quaternion buildSignRotation;

    private bool buildingNow = false;
    private float buildTimeRemaining;
    private int buildStartTime = 3;
    private GameObject halfTowerSpotInst;

    FMOD.Studio.EventInstance sfxInstance;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.COINS_UPDATED, UpdateBuildUI);
    }

    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.COINS_UPDATED, UpdateBuildUI);
    }

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
    private void ChangeTowerBuildColor()
    {
        if(previousTower == arrowPrefab)
        {
            currentImage = arrowButton.image;
        }
        else if(previousTower == magicPrefab)
        {
            currentImage = magicButton.image;
        }
        else if(previousTower == canonPrefab)
        {
            currentImage = canonButton.image;
        }
        else if(previousTower == null)
        {
            return;
        }
        currentImage.color = red;
    }


    private void ChangeEnableColor()
    {
        arrowButton.enabled = (Managers.Player.coins >= arrowCost) ? true : false;
        magicButton.enabled = (Managers.Player.coins >= magicCost) ? true : false;
        canonButton.enabled = (Managers.Player.coins >= canonCost) ? true : false;
        arrowButton.image.color = (Managers.Player.coins >= arrowCost) ? white : gray;
        magicButton.image.color = (Managers.Player.coins >= magicCost) ? white : gray;
        canonButton.image.color = (Managers.Player.coins >= canonCost) ? white : gray;
        arrowCostText.color = (Managers.Player.coins >= arrowCost) ? yellow : gray;
        magicCostText.color = (Managers.Player.coins >= magicCost) ? yellow : gray;
        canonCostText.color = (Managers.Player.coins >= canonCost) ? yellow : gray;
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
        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.buildTimeBar);
        sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        sfxInstance.start();

        InstantiateHalfTowerSpot();
        buildTimeCanvas.SetActive(true);
        buildingNow = true;

        while(buildTimeRemaining > 0)
        {
            buildTimeRemaining -= Time.deltaTime;
            buildTimeBar.fillAmount = buildTimeRemaining / buildStartTime;
            yield return null;
            
        }

        buildTimeRemaining = buildStartTime;
        Destroy(halfTowerSpotInst);
        InstantiateTower();
        buildTimeCanvas.SetActive(false);
    }

    private void InstantiateTower()
    {
        Instantiate(selectedTower, buildSignPosition, buildSignRotation, transform.parent);
        previousTower = selectedTower;
        SetBuildCanvas(false);
    }

    private void InstantiateHalfTowerSpot()
    {
        halfTowerSpotInst = Instantiate(halfTowerSpot, buildSignPosition, buildSignRotation,transform.parent);
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

    public void UpdateBuildUI()
    {
        playerCoinsText.text = Managers.Player.coins.ToString();
        ChangeEnableColor();
        ChangeTowerBuildColor();
    }

    

    


}
