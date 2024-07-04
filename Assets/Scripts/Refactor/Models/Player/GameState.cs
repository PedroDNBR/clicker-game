using System.Collections;
using UnityEngine;

namespace RC
{
    public class GameState : MonoBehaviour
    {
        public ViewLevel viewLevel;
        public ViewInventory viewInventory;
        public EnemyController enemyController;
        public HeroController heroController;

        private void Start()
        {
            heroController.OnHeroSpawned += HeroAndEnemyFight;

            Level.instance.Init();
            Inventory.instance.Init();

            viewLevel.Init();
            viewInventory.Init();
            enemyController.Init();

            DataPersistanceController.instance.Init();
        }

        public void HeroAndEnemyFight()
        {
            StartCoroutine(HeroDamageEnemy());
            StartCoroutine(EnemyDamageHero());
        }

        private IEnumerator HeroDamageEnemy()
        {
            while (enemyController.SpawnedEnemy != null && Inventory.instance.GetHeroInField() != null)
            {
                heroController.HitHeroInField(enemyController.SpawnedEnemy.CharacterItem.BaseAttackPower);
                yield return new WaitForSeconds(enemyController.SpawnedEnemy.CharacterItem.BaseAttackSpeed);
            }
        }

        private IEnumerator EnemyDamageHero()
        {
            while (Inventory.instance.GetHeroInField() != null && enemyController.SpawnedEnemy != null)
            {
                enemyController.HitSpawnedEnemy(Inventory.instance.GetHeroInField().GetTotalDamage());
                yield return new WaitForSeconds(Inventory.instance.GetHeroInField().BaseAttackSpeed);
            }
        }
    }
}
