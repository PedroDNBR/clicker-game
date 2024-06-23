using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Armor/BaseArmor")]
public class ArmorItem : BaseItem
{
    public int armorPoints;

    public override void SetUIIItemStats(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"+{armorPoints} A";
    }

    public override void SetUIForShop(GameObject itemUI)
    {
        SetUIItemSprite(itemUI);
        SetUIItemNameAndPrice(itemUI);
        SetUIIItemStats(itemUI);
    }
}
