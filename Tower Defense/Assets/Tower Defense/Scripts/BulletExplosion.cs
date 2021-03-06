﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour {

	public void DestroyExplosion()
    {
        ParticleSystem parts = GetComponent<ParticleSystem>();
        float totalDuration = parts.duration + parts.startLifetime;
        Destroy(gameObject, totalDuration);
    }
}
