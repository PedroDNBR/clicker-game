using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public interface IEnemyController
    {
        public List<IEnemyItem> EnemiesToSpawn { get; set; }

        public event Action<IEnemy> OnEnemySpawn;

        public float EnemiesPointsMultiplier { get; set; }

        public void Init();

        public void HitSpawnedEnemy(int damage);

        public void SpawnEnemyFromSave(IEnemyItem Enemy);

        public void SpawnRandonlyGeneratedEnemy();

        public void SpawnEnemy(IEnemyItem enemy = null);

        void EnemyWasDestroyed(ICharacter enemy);


    }
}

