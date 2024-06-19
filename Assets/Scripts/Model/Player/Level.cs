using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action onXpChange;

    int currentXp = 0;

    public EnemySpawner enemySpawner;

    public void Init()
    {
        enemySpawner.OnEnemySpawn += SubscribeToEnemyDestroyedAction;
    }

    void SubscribeToEnemyDestroyedAction(Enemy enemy)
    {
        enemy.onDestroyedAction += AddEnemyXp;
    }

    public void AddEnemyXp(Enemy enemy)
    {
        AddXp(enemy.xp);
    }

    public void AddXp(int xpToAdd)
    {
        currentXp += xpToAdd;
        if (onXpChange != null) onXpChange();
    }

    public int GetXp()
    {
        return currentXp;
    }

    public int GetLevel()
    {
        return Mathf.FloorToInt(currentXp / (Mathf.Pow((1.1f), (currentXp / 100)) + 100));
    }
}