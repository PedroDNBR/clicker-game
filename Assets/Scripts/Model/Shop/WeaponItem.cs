using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Weapon/BaseWeapon")]
public class WeaponItem : BaseItem
{
    public int attackPowerBoost = 20;
    public float attackSpeedBoost = .8f;
}
