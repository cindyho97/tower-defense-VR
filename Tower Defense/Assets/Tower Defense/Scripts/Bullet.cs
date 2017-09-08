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

    FMOD.Studio.EventInstance sfxInstance;

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
            if(gameObject.name.Contains("MagicBall"))
            {
                sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.magicBall);
                sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                sfxInstance.start();
                //FMODUnity.RuntimeManager.PlayOneShotAttached(Managers.AudioMan.magicBall,gameObject);
            }
            else if (gameObject.name.Contains("Arrow"))
            {
                
                sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.arrowDamage);
                sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                sfxInstance.start();
                //FMODUnity.RuntimeManager.PlayOneShotAttached(Managers.AudioMan.arrowDamage,gameObject);
            }
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
        else // Bullet from canon (area explosion)
        {
            sfxInstance = FMODUnity.RuntimeManager.CreateInstance(Managers.AudioMan.canonExplosion);
            sfxInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            sfxInstance.start();
            //FMODUnity.RuntimeManager.PlayOneShotAttached(Managers.AudioMan.canonExplosion,gameObject);
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

    private void OnDestroy()
    {
        sfxInstance.release();
    }
}
