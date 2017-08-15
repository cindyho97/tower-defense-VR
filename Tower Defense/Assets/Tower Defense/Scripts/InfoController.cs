using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour {

    public Text healthText;
    public Text maxHealthText;
    public Text coinsText;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
    }

    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
    }

    private void OnHealthUpdated()
    {
        healthText.text = Managers.Player.health.ToString();
    }

    private void OnCoinsUpdated()
    {
        coinsText.text = Managers.Player.coins.ToString();
    }
}
