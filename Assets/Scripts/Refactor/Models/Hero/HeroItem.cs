using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace RC
{
    [CreateAssetMenu(menuName = "GameAssets/NPC/HeroItem")]
    public class HeroItem : BaseItem, IHeroItem
    {
        [Header("Hero Stats")]
        [JsonIgnore] public int maxBaseHealth;
        [JsonIgnore] public int baseAttackPower;
        [JsonIgnore] public float baseAttackSpeed;
        [JsonIgnore] public string largeSpritePath;
        [JsonIgnore] public string weaponId;
        [JsonIgnore] public string helmetId;
        [JsonIgnore] public string chestplateId;
        [JsonIgnore] public string leggingsId;
        [JsonIgnore] public string bootsId;

        public int MaxBaseHealth { get => maxBaseHealth; set => maxBaseHealth = value; }
        public int BaseAttackPower { get => baseAttackPower; set => baseAttackPower = value; }
        public float BaseAttackSpeed { get => baseAttackSpeed; set => baseAttackSpeed = value; }
        public string LargeSpritePath { get => largeSpritePath; set => largeSpritePath = value; }
        public int HealthWhenInField { get; set; }
        public int ShopIndex { get; set; }
        public string WeaponId { get => weaponId; set => weaponId = value; }
        public string HelmetId { get => helmetId; set => helmetId = value; }
        public string ChestplateId { get => chestplateId; set => chestplateId = value; }
        public string LeggingsId { get => leggingsId; set => leggingsId = value; }
        public string BootsId { get => bootsId; set => bootsId = value; }
        public bool Owned { get; set; }

        public void SetUIIBaseItemStats(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{MaxBaseHealth} HP\n{BaseAttackPower} AT\n{BaseAttackSpeed} SP";
        }

        public override void SetUIIItemStats(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{MaxBaseHealth} HP\n{GetTotalDamage()} AT\n{GetTotalAttackSpeed()} SP";
        }

        public int GetTotalArmor()
        {
            int totalArmor = 0;

            if (!string.IsNullOrEmpty(HelmetId)) totalArmor += ((IArmorItem)Inventory.instance.GetHelmetItemByLocalId(HelmetId)).ArmorPoints;
            if (!string.IsNullOrEmpty(ChestplateId)) totalArmor += ((IArmorItem)Inventory.instance.GetChestplateItemByLocalId(ChestplateId)).ArmorPoints;
            if (!string.IsNullOrEmpty(LeggingsId)) totalArmor += ((IArmorItem)Inventory.instance.GetLeggingsItemByLocalId(LeggingsId)).ArmorPoints;
            if (!string.IsNullOrEmpty(BootsId)) totalArmor += ((IArmorItem)Inventory.instance.GetBootsItemByLocalId(BootsId)).ArmorPoints;

            return totalArmor;
        }

        public float GetTotalAttackSpeed()
        {
            if (!string.IsNullOrEmpty(WeaponId))
            {
                return (float)System.Math.Round(BaseAttackPower * ((IWeaponItem)Inventory.instance.GetWeaponItemByLocalId(WeaponId)).AttackSpeedBoost, 2);
            }

            return BaseAttackSpeed;
        }

        public int GetTotalDamage()
        {
            if (!string.IsNullOrEmpty(WeaponId))
            {
                return BaseAttackPower + ((IWeaponItem)Inventory.instance.GetWeaponItemByLocalId(WeaponId)).AttackPowerBoost;
            }

            return BaseAttackPower;
        }

        public int GetTotalDamageToTake(int damage)
        {
            float totalDamage = damage;

            if (!string.IsNullOrEmpty(HelmetId))
            {
                totalDamage -= totalDamage * (((IArmorItem)Inventory.instance.GetHelmetItemByLocalId(HelmetId)).ArmorPoints / 100);
            }

            if (!string.IsNullOrEmpty(ChestplateId))
            {
                totalDamage -= totalDamage * (((IArmorItem)Inventory.instance.GetChestplateItemByLocalId(ChestplateId)).ArmorPoints / 100);
            }

            if (!string.IsNullOrEmpty(LeggingsId))
            {
                totalDamage -= totalDamage * (((IArmorItem)Inventory.instance.GetLeggingsItemByLocalId(LeggingsId)).ArmorPoints / 100);
            }

            if (!string.IsNullOrEmpty(BootsId))
            {
                totalDamage -= totalDamage * (((IArmorItem)Inventory.instance.GetBootsItemByLocalId(BootsId)).ArmorPoints / 100);
            }

            return Mathf.CeilToInt(totalDamage);
        }
    }
}
