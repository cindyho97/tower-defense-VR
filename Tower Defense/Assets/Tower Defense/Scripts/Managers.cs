using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

	public static PlayerManager Player { get; private set; }
    public static EnemyManager EnemyMan { get; private set; }
    public static AudioManager AudioMan { get; private set; }

    private List<IGameManager> startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        EnemyMan = GetComponent<EnemyManager>();
        AudioMan = GetComponent<AudioManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(EnemyMan);
        startSequence.Add(AudioMan);

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
