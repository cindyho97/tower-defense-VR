using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WatchTower : MonoBehaviour, IPointerClickHandler {

    private GameObject player;
    private Vector3 cameraStartPosition;
    private bool playerAtWatchSpot;
    private GameObject groundSymbol;

    public GameObject cameraPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraStartPosition = GameObject.FindGameObjectWithTag("CameraStartPosition").transform.position;
        if (gameObject.name.Contains("WatchSpot"))
        {
            groundSymbol = transform.GetChild(0).gameObject;
        }
        player.transform.position = cameraStartPosition;
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if(playerAtWatchSpot && player.transform.position != cameraPosition.transform.position)
        {
            playerAtWatchSpot = false;
            groundSymbol.SetActive(true);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        player.transform.position = cameraPosition.transform.position;
        
        if (gameObject.name.Contains("WatchSpot"))
        {
            playerAtWatchSpot = true;
            groundSymbol.SetActive(false);
        }
    }
    
}
