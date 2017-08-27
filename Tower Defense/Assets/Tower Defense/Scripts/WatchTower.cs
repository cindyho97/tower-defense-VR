using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WatchTower : MonoBehaviour, IPointerClickHandler {

    private GameObject player;
    public GameObject cameraPosition;
    private Vector3 cameraStartPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraStartPosition = GameObject.FindGameObjectWithTag("CameraStartPosition").transform.position;
        player.transform.position = cameraStartPosition;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        player.transform.position = cameraPosition.transform.position;
    }
    
}
