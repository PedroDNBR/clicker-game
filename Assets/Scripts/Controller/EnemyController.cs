using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public Button enemyHitted;

    public void Init()
    {
        enemySpawner.SpawnEnemy();
        enemyHitted.onClick.AddListener(HitSpawnedEnemy);
    }

    void HitSpawnedEnemy()
    {
        if (enemySpawner.GetSpawnedEnemy() == null) return;
        enemySpawner.GetSpawnedEnemy().TakeDamage(10);
    }
}
