using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private int pathNodeIndex = 0;
    private Transform targetPathNode;

    public GameObject path;
    public float speed = 2f;
	
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

        if(targetPathNode == null)
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
    }

    public void TakeDamage(int damage)
    {
        // Enemy hit by bullet
    }


}
