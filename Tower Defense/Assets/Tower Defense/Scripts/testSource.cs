using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSource : MonoBehaviour {

	
	void Start () {
        Debug.Log("playing test");
        FMODUnity.RuntimeManager.PlayOneShotAttached(Managers.AudioMan.TEST,gameObject);
	}
}
