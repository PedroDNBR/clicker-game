using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int maxHealth = 100;
    int health;

    public int index = -1;

    public int xp = 10;
    public int gold = 10;

    public event Action<Enemy> onDestroyedAction;

    public event Action onTookDamageAction;


    private void Start()
    {
        health = maxHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthPercentage01()
    {
        return Mathf.InverseLerp(0, maxHealth, health);
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void Heal(int heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        if (onDestroyedAction != null) onTookDamageAction();
        if (health <= 0) 
        {
            if (onDestroyedAction != null) onDestroyedAction(this);
        }
    }
}
