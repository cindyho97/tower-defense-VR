using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    private Transform weaponTransform;

    private float fireCooldown = 1f;
    private float fireCooldownLeft = 0f;

    public float range = 20f;
    public GameObject bulletPrefab;

    public int cost = 1;

    public int damage = 1;
    public float radius = 0;

	// Use this for initialization
	void Start () {
        weaponTransform = transform.Find("Weapon");
	}
	
	// Update is called once per frame
	void Update () {
        FindNearestEnemy();
	}

    private void FindNearestEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        Enemy nearestEnemy = null;
        float distance = Mathf.Infinity; // Highest distance at beginning of comparison


        foreach(Enemy enemy in enemies)
        {
            float distTowerEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if(nearestEnemy == null || distTowerEnemy < distance)
            {
                nearestEnemy = enemy;
                distance = distTowerEnemy; 
            }
        }

        if(nearestEnemy == null)
        {
            // No enemies left?
            return;
        }

        RotateWeapon(nearestEnemy);
    }

    private void RotateWeapon(Enemy nearestEnemy)
    {
        Vector3 dir = nearestEnemy.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        weaponTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);

        fireCooldownLeft -= Time.deltaTime;
        if (fireCooldownLeft <= 0 && dir.magnitude <= range)
        {
            ShootAt(nearestEnemy);
            fireCooldownLeft = fireCooldown;
        }
    }
    
    private void ShootAt(Enemy nearestEnemy)
    {
        // TODO: fire bullet from tip
        GameObject bullet = Instantiate(bulletPrefab, weaponTransform.position, weaponTransform.rotation) as GameObject;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = nearestEnemy.transform;
        bulletScript.damage = damage;
        // Canon tower
        bulletScript.radius = radius;
    }
}
