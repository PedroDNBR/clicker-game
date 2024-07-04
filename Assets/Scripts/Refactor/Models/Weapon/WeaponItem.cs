using TMPro;
using UnityEngine;

namespace RC
{
    [CreateAssetMenu(menuName = "GameAssets/NPC/WeaponItem")]
    public class WeaponItem : BaseItem, IWeaponItem
    {
        [Header("Weapon Stats")]
        public int attackPowerBoost;
        public float attackSpeedBoost;

        public int AttackPowerBoost { get => attackPowerBoost; set => attackPowerBoost = value; }
        public float AttackSpeedBoost { get => attackSpeedBoost; set => attackSpeedBoost = value; }

        public override void SetUIIItemStats(GameObject itemUI)
        {
            float attackSpeedBoostCalculated = (100 - (AttackSpeedBoost * 100));
            string attackSpeedBoostCalculatedString = $"+{attackSpeedBoostCalculated}";
            if (attackSpeedBoostCalculated < 0) attackSpeedBoostCalculatedString = $"-{attackSpeedBoostCalculated * -1}";
            itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"+{AttackPowerBoost} AT\n{attackSpeedBoostCalculatedString} SP";
        }

        public override void SetUIForShop(GameObject itemUI)
        {
            SetUIItemSprite(itemUI);
            SetUIItemNameAndPrice(itemUI);
            SetUIIItemStats(itemUI);
        }
    }
}
