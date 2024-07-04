using UnityEngine;

namespace RC
{
    public interface IHeroItem : IBaseCharacter
    {
        public string LargeSpritePath { get; set; }

        public int HealthWhenInField { get; set; }

        public int ShopIndex { get; set; }

        public string WeaponId { get; set; }

        public string HelmetId { get; set; }
        public string ChestplateId { get; set; }
        public string LeggingsId { get; set; }
        public string BootsId { get; set; }

        public bool Owned { get; set; }

        public int GetTotalDamage();

        public float GetTotalAttackSpeed();

        public int GetTotalDamageToTake(int damage);

        public int GetTotalArmor();
    }
}
