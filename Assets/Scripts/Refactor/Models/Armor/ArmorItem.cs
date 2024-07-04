using TMPro;
using UnityEngine;

namespace RC
{
    [CreateAssetMenu(menuName = "GameAssets/NPC/ArmorItem")]
    public class ArmorItem : BaseItem, IArmorItem
    {
        [Header("Armor Stats")]
        public int armorPoints;

        public int ArmorPoints { get => armorPoints ; set => armorPoints = value; }

        public override void SetUIIItemStats(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"+{ArmorPoints} A";
        }

        public override void SetUIForShop(GameObject itemUI)
        {
            SetUIItemSprite(itemUI);
            SetUIItemNameAndPrice(itemUI);
            SetUIIItemStats(itemUI);
        }
    }
}
