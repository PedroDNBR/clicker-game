using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Level level;
    public Inventory inventory;

    public ViewLevel viewLevel;
    public ViewInventory viewInventory;
    public EnemyController enemyController;
    public HeroController heroController;

    private void Start()
    {
        level.Init();
        inventory.Init();

        viewLevel.Init();
        viewInventory.Init();
        enemyController.Init();
        heroController.OnHeroSpawned += HeroAndEnemyFight;
    }

    void HeroAndEnemyFight()
    {
        StartCoroutine(HeroDamageEnemy());
        StartCoroutine(EnemyDamageHero());
    }

    private IEnumerator HeroDamageEnemy()
    {
        while (enemyController.enemySpawner.GetSpawnedEnemy() != null)
        {
            heroController.HitHeroInField(enemyController.enemySpawner.GetSpawnedEnemy().GetEnemyItem().baseAttackPower);
            yield return new WaitForSeconds(enemyController.enemySpawner.GetSpawnedEnemy().GetEnemyItem().baseAttackSpeed);
        }
    }

    private IEnumerator EnemyDamageHero()
    {
        while (inventory.GetHeroInField() != null)
        {
            enemyController.HitSpawnedEnemy(inventory.GetHeroInField().GetTotalDamage());
            yield return new WaitForSeconds(inventory.GetHeroInField().baseAttackSpeed);
        }
    }
}
