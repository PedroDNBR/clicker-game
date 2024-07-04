using System;
using UnityEngine;

namespace RC
{
    public interface ICharacter
    {
        public IBaseCharacter CharacterItem { get; set; }

        public int Health { get; set; }

        public event Action<ICharacter> onDestroyedAction;

        public abstract event Action onTookDamageAction;

        public abstract void TakeDamage(int damage);

        public abstract int GetMaxHealth();

        public abstract float GetHealthPercentage01();

        public abstract void Heal(int heal);

        public abstract void SetCharacterItem(IBaseCharacter enemy);

        public void SetSprite(Sprite sprite);
    }
}

