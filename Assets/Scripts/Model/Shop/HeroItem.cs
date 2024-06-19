using System.Collections;
using System.Collections.Generic;
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
}
