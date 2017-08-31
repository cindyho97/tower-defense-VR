using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WatchTower : MonoBehaviour, IPointerClickHandler {

    private GameObject player;
    private Vector3 cameraStartPosition;
    private bool playerAtWatchSpot;
    private Renderer rend;
    private Collider coll;

    public GameObject cameraPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraStartPosition = GameObject.FindGameObjectWithTag("CameraStartPosition").transform.position;
        rend = gameObject.GetComponent<Renderer>();
        coll = gameObject.GetComponent<Collider>();
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
            rend.enabled = true;
            coll.enabled = true;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        player.transform.position = cameraPosition.transform.position;
        
        if (gameObject.name.Contains("WatchSpot"))
        {
            playerAtWatchSpot = true;
            rend.enabled = false;
            coll.enabled = false;
        }
    }
    
}
