using System;
using UnityEngine;

namespace RC
{
    public class Level : MonoBehaviour, ILevel
    {
        public static Level instance;

        int currentXp = 0;

        public int CurrentXp {
            get => currentXp;
            set
            {
                currentXp = value;
                if (onXpChange != null) onXpChange();
            }
        }

        public Level()
        {
            instance = this;
        }

        public void Init()
        {
            EnemyController.instance.OnEnemySpawn += SubscribeToEnemyDestroyedAction;
        }

        public event Action onXpChange;

        public void AddEnemyXp(ICharacter enemy)
        {
            AddXp((enemy.CharacterItem as EnemyItem).Xp);
        }

        public void AddXp(int xpToAdd)
        {
            int levelBefore = GetLevel();
            currentXp += xpToAdd;
            if (onXpChange != null) onXpChange();
            if (levelBefore < GetLevel() && HeroController.instance.Hero != null) HeroController.instance.Hero.SetHealthToMax();
        }

        public int GetLevel()
        {
            return Mathf.FloorToInt(currentXp / (Mathf.Pow((1.1f), (currentXp / 100)) + 100));
        }

        public void SubscribeToEnemyDestroyedAction(ICharacter enemy)
        {
            enemy.onDestroyedAction += AddEnemyXp;
        }
    }
}
