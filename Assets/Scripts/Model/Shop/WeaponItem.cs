using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Weapon/BaseWeapon")]
public class WeaponItem : BaseItem
{
    public int attackPowerBoost = 20;
    public float attackSpeedBoost = .8f;

    public override void SetUIIItemStats(GameObject itemUI)
    {
        float attackSpeedBoostCalculated = (100 - (attackSpeedBoost * 100));
        string attackSpeedBoostCalculatedString = $"+{attackSpeedBoostCalculated}";
        if (attackSpeedBoostCalculated < 0) attackSpeedBoostCalculatedString = $"-{attackSpeedBoostCalculated * -1}";
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"+{attackPowerBoost} AT\n{attackSpeedBoostCalculatedString} SP";
    }

    public override void SetUIForShop(GameObject itemUI)
    {
        SetUIItemSprite(itemUI);
        SetUIItemNameAndPrice(itemUI);
        SetUIIItemStats(itemUI);
    }
}
