using UnityEngine;

namespace RC
{
    public class Enemy : Character, IEnemy
    {
        public int Index { get; set; }

        public override void SetCharacterItem(IBaseCharacter enemy)
        {
            base.SetCharacterItem(enemy);
            Health = CharacterItem.MaxBaseHealth;
            SetSprite(EnemyController.instance.enemiesLargeSpriteList[(CharacterItem as IEnemyItem).LargeSpriteIndex]);
        }
    }
}
