using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Material currentMaterial;
    public Material outlineMaterial;
    private Material outlineWithTexture;
    public static GameObject currentObject;

    private void Awake()
    {
        currentMaterial = GetComponent<MeshRenderer>().material;
        outlineWithTexture = Instantiate(outlineMaterial);
        outlineWithTexture.mainTexture = currentMaterial.mainTexture;
        currentObject = GameObject.FindGameObjectWithTag("CameraStartPosition").transform.parent.gameObject;
    }

    public void SetGazedAt(bool gazedAt)
    {
        GetComponent<MeshRenderer>().material = gazedAt ? outlineWithTexture : currentMaterial;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Can't click on current object
        if (GetComponent<WatchTower>() && eventData.pointerCurrentRaycast.gameObject == currentObject)
        { 
            return;
        }

        SetGazedAt(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetGazedAt(false);
    }
}
