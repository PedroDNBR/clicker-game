using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Hero")]
public class HeroItem : BaseItem
{
    public int maxBaseHealth = 100;
    public int baseAttackPower = 10;
    public float baseAttackSpeed = 1f;

    [Header("Inventory")]
    public WeaponItem weapon;

    public HelmetItem helmet;
    public ChestplateItem chestplate;
    public LeggingsItem leggings;
    public BootsItem boots;

    public void SetUIIHeroStats(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"{maxBaseHealth} HP\n{baseAttackPower} AT\n{baseAttackSpeed} SP";
    }
}
