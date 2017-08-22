using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private int pathNodeIndex = 0;
    private Transform targetPathNode;
    protected int health;

    public GameObject path;
    public float speed;
    public int startHealth;
    public int coinValue;
    public Image healthBarImage;

    private void Start()
    {
        path = GameObject.Find("Path");
        health = startHealth;
    }

    // Update is called once per frame
    void Update () {
        if(targetPathNode == null)
        {
            GetNextPathNode();
        }
        if(targetPathNode != null)
        {
            MoveTowardsNode();
        }
        
	}

    private void GetNextPathNode()
    {
        if (pathNodeIndex < path.transform.childCount)
        {
            targetPathNode = path.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            targetPathNode = null;
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
        Managers.EnemyMan.enemyCount--;
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        
        // Enemy hit by bullet
        health -= damage;

        healthBarImage.fillAmount = (float)health / startHealth;
        
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    { 
        Managers.Player.UpdateCoins(coinValue);
        Managers.EnemyMan.enemyCount--;
        // Start death animation
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }


}
