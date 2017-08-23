using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower {

	public CanonTower()
    {
        range = 6f;
        cost = 125;
        damage = 4;
        explosionRadius = 4f;
    }
}
