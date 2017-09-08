using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,IGameManager {

    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int coins { get; private set; }
	
    public void Startup()
    {
        UpdateData(20, 20, 400);
        status = ManagerStatus.Started;
    }

    public void UpdateData(int health, int maxHealth, int coins)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.coins = coins;
    }

    public void UpdateHealth(int damage)
    {
        health += damage;

        if(health <= 0)
        {
            health = 0;
        }
        
        if(health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void UpdateCoins(int value)
    {
        coins += value;

        if (coins < 0)
        {
            coins = 0;
        }

        Messenger.Broadcast(GameEvent.COINS_UPDATED);
    }

    public void Respawn()
    {
        UpdateData(20, 20, 400);
    }
}
