using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColor : MonoBehaviour {

    public Material inactiveMaterial;
    public Material outlineMaterial;

	// Use this for initialization
	void Start () {
        SetGazedAt(false);
	}
	
    public void SetGazedAt(bool gazedAt)
    {
        if (inactiveMaterial != null && outlineMaterial != null)
        {
            GetComponent<Renderer>().material = gazedAt ? outlineMaterial : inactiveMaterial;
        }
    }

    public void MoveForward()
    {
        transform.Translate(1.0f, 0, 0);
    }
}
