using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeParticle : MonoBehaviour {

    private ParticleSystem particle;
    FMOD.Studio.EventInstance fireworkSfx;
    private bool playedSfx = false;

    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();
        fireworkSfx = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.firework);
        fireworkSfx.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale < 0.01f)
        {
            particle.Simulate(Time.unscaledDeltaTime, true, false);
            if (particle.particleCount > 0)
            {
                if (!playedSfx)
                {
                    fireworkSfx.start();
                    playedSfx = true;
                }
            }
        }
    }
}
