using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private int pathNodeIndex = 0;
    private Transform targetPathNode;
    private Animator anim;
    
    protected int health;

    public GameObject path;
    public float speed;
    public int startHealth;
    public int coinValue;
    public bool isAlive = true;
    public Image healthBarImage;

    private void Start()
    {
        path = GameObject.Find("PathNodes");
        health = startHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
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
        Debug.Log("Reached Goal!");
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

        FMODUnity.RuntimeManager.PlayOneShot(Managers.AudioMan.coinDrop);
        anim.SetBool("Die", true);
        Managers.Player.UpdateCoins(coinValue);
        Managers.EnemyMan.enemyCount--;

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void DisableHealthBar()
    {
        CanvasGroup healthCanvas = healthBarImage.transform.parent.GetComponentInParent<CanvasGroup>();
        healthCanvas.alpha = 0;
        healthCanvas.interactable = false;
        healthCanvas.blocksRaycasts = false;
    }


}
