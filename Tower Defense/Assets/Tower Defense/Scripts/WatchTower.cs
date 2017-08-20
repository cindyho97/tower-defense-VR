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

    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        player.transform.position = cameraPosition.transform.position;
        player.transform.rotation = cameraPosition.transform.rotation;
    }
    
}
