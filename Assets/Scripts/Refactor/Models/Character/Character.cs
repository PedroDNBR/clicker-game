using System;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class Character : MonoBehaviour, ICharacter
    {
        public virtual IBaseCharacter CharacterItem { get; set; }
        public int Health { get; set; }

        public virtual event Action<ICharacter> onDestroyedAction;
        public virtual event Action onTookDamageAction;

        public float GetHealthPercentage01()
        {
            return Mathf.InverseLerp(0, CharacterItem.MaxBaseHealth, Health);
        }

        public int GetMaxHealth()
        {
            if (CharacterItem == null) return -1;

            return CharacterItem.MaxBaseHealth;
        }

        public void Heal(int heal)
        {
            Health = Mathf.Clamp(Health + heal, 0, CharacterItem.MaxBaseHealth);
        }

        public virtual void SetCharacterItem(IBaseCharacter enemy)
        {
            CharacterItem = enemy;
        }

        public void SetSprite(Sprite sprite)
        {

            Image image = gameObject.GetComponent<Image>();
            image.sprite = sprite;
        }

        public virtual void TakeDamage(int damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, CharacterItem.MaxBaseHealth);
            if (onTookDamageAction != null) onTookDamageAction();
            if (Health <= 0)
            {
                if (onDestroyedAction != null) onDestroyedAction(this);
            }
        }
    }
}

