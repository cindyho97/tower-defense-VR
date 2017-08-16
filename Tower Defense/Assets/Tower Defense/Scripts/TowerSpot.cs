using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public void OnTowerSelect()
    {
        Debug.Log("Tower spot clicked");
        BuildManager bm = GameObject.FindObjectOfType<BuildManager>();
        if( bm.selectedTower != null)
        {
            // FIXME: we assume that we are an object nested in a parent
            Instantiate(bm.selectedTower, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

}
