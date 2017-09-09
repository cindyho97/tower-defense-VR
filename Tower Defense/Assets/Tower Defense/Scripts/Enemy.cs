using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private int pathNodeIndex = 0;
    private Transform targetPathNode;
    private Animator anim;
    private Transform player;
    
    protected int health;

    public GameObject path;
    public float speed;
    public int startHealth;
    public int coinValue;
    public bool isAlive = true;
    public Image healthBarImage;
    public Transform healthBarCanvas;

    private void Start()
    {
        path = GameObject.Find("PathNodes");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = startHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        RotateHealthBar();
        if(targetPathNode == null)
        {
            GetNextPathNode();
        }
        if(targetPathNode != null && isAlive)
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
            // Move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRot = Quaternion.LookRotation(dir); // Look in direction that enemy is moving
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5); // Smooth out rotation
        }
    }

    private void ReachedGoal()
    {
        // Enemy arrives at castle
        Managers.Player.UpdateHealth(-2);
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
            isAlive = false;

            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        DisableHealthBar();

        GameObject[] buildManagers = GameObject.FindGameObjectsWithTag("BuildManager");
        for(int i = 0; i < buildManagers.Length; i++)
        {
            buildManagers[i].GetComponent<BuildManager>().UpdateBuildUI();
        }
   
        anim.SetBool("Die", true);
        
        Managers.Player.UpdateCoins(coinValue);
        Managers.EnemyMan.enemyCount--;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void DisableHealthBar()
    {
        CanvasGroup healthCGroup = healthBarCanvas.GetComponent<CanvasGroup>();
        healthCGroup.alpha = 0;
        healthCGroup.interactable = false;
        healthCGroup.blocksRaycasts = false;
    }

    private void RotateHealthBar()
    {
        Vector3 dir = player.position - healthBarCanvas.position;
        Quaternion barRot = Quaternion.LookRotation(dir);
        healthBarCanvas.rotation = barRot;
    }


}
