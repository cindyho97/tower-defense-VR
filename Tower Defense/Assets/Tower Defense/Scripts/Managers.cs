using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

	public static PlayerManager Player { get; private set; }

    private List<IGameManager> startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach(IGameManager manager in startSequence)
        {
            manager.Startup();
        }

        yield return null;
 
        //Debug.Log("All managers started up!");

    }
}
