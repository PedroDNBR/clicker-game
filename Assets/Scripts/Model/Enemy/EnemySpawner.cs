using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemiesToSpawn = new List<Enemy>();

    public event Action<Enemy> OnEnemySpawn;

    public Transform spawnerPivot;

    Enemy spawnedEnemy = null;

    void SetEnemiesToSpawn(List<Enemy> enemiesToSpawn)
    {
        this.enemiesToSpawn = enemiesToSpawn;
    }

    void AddEnemyToSpawn(Enemy enemyToSpawn)
    {
        enemiesToSpawn.Add(enemyToSpawn);
    }

    void RemoveEnemyToSpawn(Enemy enemy)
    {
        enemiesToSpawn.RemoveAt(enemy.index);
    }

    public void SpawnEnemy(Enemy enemy = null)
    {
        if(enemy != null)
            spawnedEnemy = Instantiate(enemy, spawnerPivot);
        else
        {
            if (enemiesToSpawn.Count <= 0) return;
            spawnedEnemy = Instantiate(enemiesToSpawn[0], spawnerPivot);
            spawnedEnemy.index = 0;
        }

        spawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
        if(OnEnemySpawn != null) OnEnemySpawn(spawnedEnemy);
    }

    void EnemyWasDestroyed(Enemy enemy)
    {
        enemy.onDestroyedAction -= EnemyWasDestroyed;
        RemoveEnemyToSpawn(enemy);
        Destroy(enemy.gameObject);
        SpawnEnemy();
    }

    public Enemy GetSpawnedEnemy()
    {
        return spawnedEnemy;
    }
}
