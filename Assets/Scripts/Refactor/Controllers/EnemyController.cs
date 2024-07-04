using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class EnemyController : MonoBehaviour, IEnemyController
    {
        public static EnemyController instance;

        List<IEnemyItem> enemiesToSpawn = new List<IEnemyItem>();
        public List<IEnemyItem> EnemiesToSpawn { get => enemiesToSpawn; set => enemiesToSpawn = value; }

        public Enemy enemyTemplate;
        public Transform spawnerPivot;
        Enemy spawnedEnemy = null;
        public Enemy SpawnedEnemy { get => spawnedEnemy; set => spawnedEnemy = value; }
        float enemiesPointsMultiplier = 1;
        public float EnemiesPointsMultiplier { get => enemiesPointsMultiplier; set => enemiesPointsMultiplier = value; }
        public Sprite[] enemiesLargeSpriteList;
        public Button enemyHitted;

        public event Action<IEnemy> OnEnemySpawn;

        [Header("Random Enemy Values")]
        public int baseMinHealth = 100;
        public int baseMaxHealth = 200;

        public int baseMinAttackPower = 1;
        public int baseMaxAttackPower = 5;

        public float baseMinAttackSpeed = .9f;
        public float baseMaxAttackSpeed = 5f;

        public int xpPointsDivisor = 20;
        public int goldPointsDivisor = 60;

        public EnemyController()
        {
            instance = this;
        }

        public void EnemyWasDestroyed(ICharacter enemy)
        {
            enemy.onDestroyedAction -= EnemyWasDestroyed;
            // RemoveEnemyToSpawn(enemy);
            Destroy((enemy as Enemy).gameObject);
            EnemiesPointsMultiplier += .01f;
            SpawnRandonlyGeneratedEnemy();
        }

        public void HitSpawnedEnemy(int damage)
        {
            if (SpawnedEnemy == null) return;
            SpawnedEnemy.TakeDamage(damage);
        }

        public void Init()
        {
            if (SpawnedEnemy == null)
            {
                SpawnRandonlyGeneratedEnemy();
            }
            enemyHitted.onClick.AddListener(() => HitSpawnedEnemy(10));
        }

        public void SpawnEnemy(IEnemyItem enemy = null)
        {
            if (enemy != null)
            {
                SpawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
                SpawnedEnemy.SetCharacterItem(enemy);
            }
            else
            {
                if (EnemiesToSpawn.Count <= 0) return;
                SpawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
                SpawnedEnemy.Index = 0;
                SpawnedEnemy.SetCharacterItem(EnemiesToSpawn[0]);
            }

            SpawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
            if (OnEnemySpawn != null) OnEnemySpawn(SpawnedEnemy);
        }

        public void SpawnEnemyFromSave(IEnemyItem Enemy)
        {
            if (SpawnedEnemy != null)
            {
                Destroy(SpawnedEnemy.gameObject);
            }

            SpawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);

            SpawnedEnemy.SetCharacterItem(Enemy);

            SpawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
            if (OnEnemySpawn != null) OnEnemySpawn(SpawnedEnemy);
        }

        public void SpawnRandonlyGeneratedEnemy()
        {
            SpawnedEnemy = Instantiate(enemyTemplate, spawnerPivot);
            EnemyItem generatedEnemy = Instantiate((EnemyItem)ScriptableObject.CreateInstance(typeof(EnemyItem)));
            generatedEnemy.MaxBaseHealth = Mathf.CeilToInt(UnityEngine.Random.Range(baseMinHealth, baseMaxHealth) * EnemiesPointsMultiplier);
            generatedEnemy.BaseAttackPower = Mathf.CeilToInt(UnityEngine.Random.Range(baseMinAttackPower, baseMaxAttackPower) * EnemiesPointsMultiplier);
            generatedEnemy.BaseAttackSpeed = UnityEngine.Random.Range(baseMinAttackSpeed, baseMaxAttackSpeed) / EnemiesPointsMultiplier;
            generatedEnemy.LargeSpriteIndex = UnityEngine.Random.Range(0, enemiesLargeSpriteList.Length - 1);

            generatedEnemy.Xp = Mathf.CeilToInt((generatedEnemy.MaxBaseHealth + generatedEnemy.BaseAttackPower) / xpPointsDivisor);
            generatedEnemy.Gold = Mathf.CeilToInt((generatedEnemy.MaxBaseHealth + generatedEnemy.BaseAttackPower) / goldPointsDivisor);

            SpawnedEnemy.SetCharacterItem(generatedEnemy);

            SpawnedEnemy.onDestroyedAction += EnemyWasDestroyed;
            if (OnEnemySpawn != null) OnEnemySpawn(SpawnedEnemy);
        }
    }
}

