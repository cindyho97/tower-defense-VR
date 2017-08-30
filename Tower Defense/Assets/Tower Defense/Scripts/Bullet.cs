using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject explosionPrefab;
    public Transform target;
    public float speed = 10f;
    public int damage;
    // Canon tower
    public float radius = 0;

    private void Update()
    {
        if(target != null)
        {
            ShootBullet();
        }
        CheckTargetExists();
    }

    private void ShootBullet()
    {
        Vector3 dir = target.position - transform.localPosition;

        float distThisFrame = speed * Time.deltaTime; // Distance per frame

        if(dir.magnitude <= distThisFrame)
        {
            // Bullet reached target
            BulletHit();
        }
        else
        {
            // Move towards target
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir); // Look in direction that enemy is moving
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 0.5f); // Smooth out rotation
        }
    }

    private void BulletHit()
    {
        GameObject explosion = Instantiate(explosionPrefab, target.position, target.rotation) as GameObject;
        explosion.GetComponent<BulletExplosion>().DestroyExplosion();

        if (radius == 0)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
        else // Bullet with area explosion effect
        {
            
            Collider[] cols = Physics.OverlapSphere(transform.position, radius); // Return array of colliders that bullet collides with

            foreach(Collider collider in cols)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            
        }
        Destroy(gameObject);
    }

    private void CheckTargetExists()
    {
        if(target == null)
        {
            // Target out of reach!
            Destroy(gameObject);
            return;
        }
    }
}
