using System;
using UnityEngine;

namespace RC
{
    public class Hero : Character, IHero
    {
        IBaseCharacter characterItem = null;
        public string localHeroId;

        public override IBaseCharacter CharacterItem { get => characterItem; set => SetHeroItem(value); }

        public event Action<IHero, string> onHeroDestroyedAction;
        public override event Action onTookDamageAction;

        void SetHeroItem(IBaseCharacter value)
        {
            characterItem = value;
            if ((characterItem as IHeroItem).HealthWhenInField > 0)
                Health = (characterItem as IHeroItem).HealthWhenInField;
            else
                Health = characterItem.MaxBaseHealth;

            SetSprite(Resources.Load<Sprite>((characterItem as IHeroItem).LargeSpritePath));
        }

        public override void SetCharacterItem(IBaseCharacter enemy)
        {
            base.SetCharacterItem(enemy);
            if ((CharacterItem as IHeroItem).HealthWhenInField > 0)
                Health = (CharacterItem as IHeroItem).HealthWhenInField;
            else
                Health = CharacterItem.MaxBaseHealth;

            SetSprite(Resources.Load<Sprite>((CharacterItem as IHeroItem).LargeSpritePath));
        }

        public void SetHealthToMax()
        {
            if (CharacterItem == null) return;
            Health = CharacterItem.MaxBaseHealth;
            if (onTookDamageAction != null) onTookDamageAction();
        }

        public override void TakeDamage(int damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, CharacterItem.MaxBaseHealth);
            (CharacterItem as IHeroItem).HealthWhenInField = Health;
            if (onTookDamageAction != null) onTookDamageAction();
            if (Health <= 0)
            {
                if (onHeroDestroyedAction != null) onHeroDestroyedAction(this, localHeroId);
            }
        }
    }
}
