using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<EnemyItem> enemiesToSpawn = new List<EnemyItem>();

    public event Action<Enemy> OnEnemySpawn;

    public Enemy enemyTemplate;
    public EnemyItem enemyItemTemplate;

    public Transform spawnerPivot;

    Enemy spawnedEnemy = null;

    float enemiesPointsMultiplier = 1;

    public Sprite[] enemiesLargeSpriteList;

    public EnemySpawner()
    {
        instance = this;
    }

    void SetEnemiesToSpawn(List<EnemyItem> enemiesToSpawn)
    {
        this.enemiesToSpawn = enemiesToSpawn;
    }

    void AddEnemyToSpawn(EnemyItem enemyToSpawn)
    {
        enemiesToSpawn.Add(enemyToSpawn);
    }

    void RemoveEnemyToSpawn(Enemy enemy)
    {
        enemiesToSpawn.RemoveAt(enemy.GetIndex());
    }

    public void SpawnRandonlyGeneratedEnemy()
    {
        spawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
        EnemyItem generatedEnemy = Instantiate(enemyItemTemplate);
        generatedEnemy.maxBaseHealth = Mathf.CeilToInt(UnityEngine.Random.Range(100, 200) * enemiesPointsMultiplier);
        generatedEnemy.baseAttackPower = Mathf.CeilToInt(UnityEngine.Random.Range(1, 5) * enemiesPointsMultiplier);
        generatedEnemy.baseAttackSpeed = UnityEngine.Random.Range(.9f, 5f) / enemiesPointsMultiplier;
        generatedEnemy.largeSprite = enemiesLargeSpriteList[UnityEngine.Random.Range(0, enemiesLargeSpriteList.Length - 1)];

        generatedEnemy.xp = Mathf.CeilToInt((generatedEnemy.maxBaseHealth + generatedEnemy.baseAttackPower) / 2);
        generatedEnemy.gold = Mathf.CeilToInt((generatedEnemy.maxBaseHealth + generatedEnemy.baseAttackPower) / 20);

        spawnedEnemy.SetEnemyItem(generatedEnemy);

        spawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
        if (OnEnemySpawn != null) OnEnemySpawn(spawnedEnemy);
    }

    public void SpawnEnemy(EnemyItem enemy = null)
    {
        if(enemy != null)
        {
            spawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
            spawnedEnemy.SetEnemyItem(enemy);
        }
        else
        {
            if (enemiesToSpawn.Count <= 0) return;
            spawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
            spawnedEnemy.SetIndex(0);
            spawnedEnemy.SetEnemyItem(enemiesToSpawn[0]);
        }

        spawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
        if(OnEnemySpawn != null) OnEnemySpawn(spawnedEnemy);
    }

    void EnemyWasDestroyed(Enemy enemy)
    {
        enemy.onDestroyedAction -= EnemyWasDestroyed;
        // RemoveEnemyToSpawn(enemy);
        Destroy(enemy.gameObject);
        enemiesPointsMultiplier += .01f;
        SpawnRandonlyGeneratedEnemy();
    }

    public Enemy GetSpawnedEnemy()
    {
        return spawnedEnemy;
    }
}
