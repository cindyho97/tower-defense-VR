using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeParticle : MonoBehaviour {

    private ParticleSystem particle;

    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale < 0.01f)
        {
            particle.Simulate(Time.unscaledDeltaTime, true, false);
        }
    }
}
