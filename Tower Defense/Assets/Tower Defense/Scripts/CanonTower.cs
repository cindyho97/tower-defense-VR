using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower {

	public CanonTower()
    {
        range = 11f;
        cost = 125;
        damage = 3;
        explosionRadius = 3f;
    }
}
