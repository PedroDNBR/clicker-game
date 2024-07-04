using UnityEngine;

namespace RC
{
    [CreateAssetMenu(menuName = "GameAssets/NPC/EnemyItem")]
    public class EnemyItem : BaseItem, IEnemyItem
    {
        public int MaxBaseHealth { get; set; }

        public int BaseAttackPower { get; set; }

        public float BaseAttackSpeed { get; set; }

        public int Xp { get; set; }

        public int Gold { get; set; }

        public int LargeSpriteIndex { get; set; }
    }
}
