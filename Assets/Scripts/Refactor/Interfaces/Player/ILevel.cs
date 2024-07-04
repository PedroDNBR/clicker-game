using System;

namespace RC
{
    public interface ILevel
    {
        public event Action onXpChange;

        public int CurrentXp { get; set; }

        public void Init();

        public void SubscribeToEnemyDestroyedAction(ICharacter enemy);

        public void AddEnemyXp(ICharacter enemy);

        public void AddXp(int xpToAdd);

        public int GetLevel();
    }
}
