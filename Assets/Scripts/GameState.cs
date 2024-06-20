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
            heroController.HitHeroInField(2);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator EnemyDamageHero()
    {
        while (inventory.heroInField != null)
        {
            enemyController.HitSpawnedEnemy(inventory.heroInField.baseAttackPower);
            yield return new WaitForSeconds(inventory.heroInField.baseAttackSpeed);
        }
    }
}
