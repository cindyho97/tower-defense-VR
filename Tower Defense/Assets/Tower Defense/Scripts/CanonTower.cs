using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower {

	public CanonTower()
    {
        range = 25f;
        cost = 5;
        damage = 5;
        explosionRadius = 10f;
    }
}
