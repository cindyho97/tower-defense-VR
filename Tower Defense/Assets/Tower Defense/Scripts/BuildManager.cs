using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public GameObject selectedTower;

    public GameObject arrowTower;
    public GameObject magicTower;
    public GameObject canonTower;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTowerTypeSelect(GameObject towerPrefab)
    {
        selectedTower = towerPrefab;
    }

}
