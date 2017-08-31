using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WatchTower : MonoBehaviour, IPointerClickHandler {

    private GameObject player;
    private Vector3 cameraStartPosition;
    private bool playerAtWatchSpot;
    private GameObject groundSymbol;

    private Transform cameraPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraPosition = transform.GetChild(0);
        cameraStartPosition = GameObject.FindGameObjectWithTag("CameraStartPosition").transform.position;
        if (gameObject.name.Contains("WatchSpot"))
        {
            groundSymbol = transform.GetChild(1).gameObject;
        }
        player.transform.position = cameraStartPosition;
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if(playerAtWatchSpot && player.transform.position != cameraPosition.position)
        {
            playerAtWatchSpot = false;
            groundSymbol.SetActive(true);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        player.transform.position = cameraPosition.position;
        
        if (gameObject.name.Contains("WatchSpot"))
        {
            playerAtWatchSpot = true;
            groundSymbol.SetActive(false);
        }
    }
    
}
