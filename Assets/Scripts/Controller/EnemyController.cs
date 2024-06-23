using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public Button enemyHitted;

    public void Init()
    {
        enemySpawner.SpawnRandonlyGeneratedEnemy();
        enemyHitted.onClick.AddListener(() => HitSpawnedEnemy(10));
    }

    public void HitSpawnedEnemy(int damage)
    {
        if (enemySpawner.GetSpawnedEnemy() == null) return;
        enemySpawner.GetSpawnedEnemy().TakeDamage(damage);
    }
}
