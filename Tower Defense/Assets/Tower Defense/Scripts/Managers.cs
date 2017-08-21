using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

	public static PlayerManager Player { get; private set; }
    public static EnemyManager EnemyMan { get; private set; }

    private List<IGameManager> startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        EnemyMan = GetComponent<EnemyManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(EnemyMan);

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
