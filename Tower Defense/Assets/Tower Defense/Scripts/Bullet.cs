using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform target;
    public float speed = 10f;
    public int damage = 5;

    private void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        Vector3 dir = target.position - transform.localPosition;
        Debug.Log(target);
        float distThisFrame = speed * Time.deltaTime; // Distance per frame

        if(dir.magnitude < distThisFrame)
        {
            // Bullet reached target
            BulletHit();
        }
        else
        {
            // FIXME: bullet in right direction
            // Move towards target
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir); // Look in direction that enemy is moving
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10); // Smooth out rotation
        }
    }

    private void BulletHit()
    {
        // TODO: bullet explosion area of effect
        target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
