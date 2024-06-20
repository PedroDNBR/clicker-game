using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Weapon/BaseWeapon")]
public class WeaponItem : BaseItem
{
    public int attackPowerBoost = 20;
    public float attackSpeedBoost = .8f;

    public void SetUIIWeaponStats(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"+{attackPowerBoost} AT\n+{100 - (attackSpeedBoost * 100)} SP";
    }

    public override void SetUIForShop(GameObject itemUI)
    {
        SetUIItemSprite(itemUI);
        SetUIItemNameAndPrice(itemUI);
        SetUIIWeaponStats(itemUI);
    }
}
