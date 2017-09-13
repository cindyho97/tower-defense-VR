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

    FMOD.Studio.EventInstance sfxInstance;

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
        
        ObjectOutline.currentObject = gameObject;
        player.transform.position = cameraPosition.position;

        sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.teleport);
        sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player)); // Set position of sound source
        sfxInstance.start();
        

        if (gameObject.name.Contains("WatchSpot"))
        {
            playerAtWatchSpot = true;
            groundSymbol.SetActive(false);
        }
    }
}
