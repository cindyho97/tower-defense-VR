using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler {
    private Transform weaponTransform;
    private BuildManager buildCanvas;

    private float fireCooldown = 1.5f;
    private float fireCooldownLeft = 0f;

    public Transform bulletPosition;
    public GameObject bulletPrefab; 

    public float range;
    public int cost;
    public int damage;

   
    // Canon tower
    public float explosionRadius = 0;

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
        List<Enemy> enemiesAlive = new List<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            // Only keep living enemies
            if (enemy.isAlive)
            {
                enemiesAlive.Add(enemy);
            } 
        }

        foreach(Enemy enemy in enemiesAlive)
        {
            // Compare distance and set nearest enemy
            float distTowerEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (nearestEnemy == null || distTowerEnemy < distance)
            {
                nearestEnemy = enemy;
                distance = distTowerEnemy;
            }
        }
        enemiesAlive.Clear();

        if (nearestEnemy == null)
        {
            // No enemies left?
            return;
        }

        RotateWeapon(nearestEnemy);
    }

    private void RotateWeapon(Enemy nearestEnemy)
    {
        
        Vector3 dir = nearestEnemy.transform.position - transform.position;

        // Enemy in range
        if(dir.magnitude <= range)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);

            weaponTransform.rotation = Quaternion.Lerp(weaponTransform.rotation, lookRot, Time.deltaTime * 5f);

            fireCooldownLeft -= Time.deltaTime;
            if (fireCooldownLeft <= 0)
            {
                ShootAt(nearestEnemy);
                fireCooldownLeft = fireCooldown;
            }
        }
        
    }
    
    private void ShootAt(Enemy nearestEnemy)
    {
        if(nearestEnemy.isAlive)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation) as GameObject;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.target = nearestEnemy.transform;
            bulletScript.damage = damage;

            // Canon tower
            bulletScript.radius = explosionRadius;
        }
    }

    // Click on tower
    public void OnPointerClick(PointerEventData eventData)
    {
        buildCanvas = transform.parent.GetComponent<TowerSpot>().buildCanvas.GetComponent<BuildManager>();
        CanvasGroup buildCanvasGroup = buildCanvas.GetComponent<CanvasGroup>();
        if(buildCanvasGroup.alpha == 1) // buildCanvas is already active
        {
            buildCanvas.SetBuildCanvas(false);
        }
        else
        {
            buildCanvas.playerCoinsText.text = Managers.Player.coins.ToString();
            buildCanvas.SetBuildCanvas(true);
            buildCanvas.UpdateBuildUI();
        }
    }
}
