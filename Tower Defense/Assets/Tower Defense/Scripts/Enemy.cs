using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private int pathNodeIndex = 0;
    private Transform targetPathNode;

    public GameObject path;
    public float speed = 2f;
    public int health = 50;
    public int coinValue = 1;
	
	// Update is called once per frame
	void Update () {
        if(targetPathNode == null)
        {
            GetNextPathNode();
        }

        MoveTowardsNode();
	}

    private void GetNextPathNode()
    {
        targetPathNode = path.transform.GetChild(pathNodeIndex);
        pathNodeIndex++;

        if(targetPathNode.name ==  path.transform.GetChild(0).name + " (" + (path.transform.childCount-1) + ")")
        {
            ReachedGoal();
        }
    }

    private void MoveTowardsNode()
    {
        Vector3 dir = targetPathNode.position - transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if(dir.magnitude < distThisFrame) // Distance of enemy and node < distance enemy walks per frame
        {
            // Enemy reached node
            targetPathNode = null;
        }
        else
        {
            // TODO: smooth out rotation more
            // Move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRot = Quaternion.LookRotation(dir); // Look in direction that enemy is moving
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 2); // Smooth out rotation
        }
    }

    private void ReachedGoal()
    {
        // Enemy arrives at castle
        Debug.Log("Reached Goal!");
        Managers.Player.UpdateHealth(-20);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        // Enemy hit by bullet
        health -= damage;

        if(health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Managers.Player.UpdateCoins(coinValue);
        // Start death animation
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }


}
