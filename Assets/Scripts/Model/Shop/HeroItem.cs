using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Hero")]
public class HeroItem : BaseItem
{
    public int maxBaseHealth = 100;
    public int baseAttackPower = 10;
    public float baseAttackSpeed = 1f;

    public Sprite largeSprite;

    public int minLevelToUnlock = 0;

    private int healthWhenInField = -1;

    private int shopIndex = 0;

    [Header("Inventory")]
    public WeaponItem weapon;

    public HelmetItem helmet;
    public ChestplateItem chestplate;
    public LeggingsItem leggings;
    public BootsItem boots;

    private bool owned = false;

    public override void SetUIIItemStats(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"{maxBaseHealth} HP\n{GetTotalDamage()} AT\n{GetTotalAttackSpeed()} SP";
    }

    public int GetTotalDamage()
    {
        if(weapon != null)
        {
            return baseAttackPower + weapon.attackPowerBoost;
        }

        return baseAttackPower;
    }

    public float GetTotalAttackSpeed()
    {
        if (weapon != null)
        { 
            return (float) System.Math.Round(baseAttackPower * weapon.attackSpeedBoost, 2);
        }

        return baseAttackSpeed;
    }

    public int GetTotalDamageToTake(int damage)
    {
        float totalDamage = damage;

        if (helmet != null) totalDamage -= totalDamage * (helmet.armorPoints / 100);
        if (chestplate != null) totalDamage -= totalDamage * (chestplate.armorPoints / 100);
        if (leggings != null) totalDamage -= totalDamage * (leggings.armorPoints / 100);
        if (boots != null) totalDamage -= totalDamage * (boots.armorPoints / 100);

        return Mathf.CeilToInt(totalDamage);
    }

    public int GetTotalArmor()
    {
        int totalArmor = 0;

        if (helmet != null) totalArmor += helmet.armorPoints;
        if (chestplate != null) totalArmor += chestplate.armorPoints;
        if (leggings != null) totalArmor += leggings.armorPoints;
        if (boots != null) totalArmor += boots.armorPoints;

        return totalArmor;
    }

    public bool GetOwned() 
    { 
        return owned; 
    }

    public void SetOwned(bool owned) 
    { 
        this.owned = owned;
    }

    public int GetHealthWhenInField()
    {
        return healthWhenInField;
    }

    public void SetHealthWhenInField(int health)
    {
        healthWhenInField = health;
    }

    public int GetShopIndex()
    {
        return shopIndex;
    }

    public void SetShopIndex(int shopIndex)
    {
        this.shopIndex = shopIndex;
    }

}
