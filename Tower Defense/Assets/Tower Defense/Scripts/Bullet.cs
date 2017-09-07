using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //[FMODUnity.EventRef]
    //public string bulletSound;
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
            Quaternion targetRotation = Quaternion.LookRotation(dir); // Look in direction that enemy is moving
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth out rotation

            transform.Translate(dir.normalized * distThisFrame, Space.World);
        }
    }

    private void BulletHit()
    {
        GameObject explosion = Instantiate(explosionPrefab, target.position, target.rotation) as GameObject;
        explosion.GetComponent<BulletExplosion>().DestroyExplosion();

        if (radius == 0)
        {
            Debug.Log(gameObject.name);
            if(gameObject.name.Contains("MagicBall"))
            {
                FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.magicBall);
            }
            else if (gameObject.name.Contains("Arrow"))
            {
                FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.arrowDamage);
            }
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
        else // Bullet from canon (area explosion)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.canonExplosion);
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
