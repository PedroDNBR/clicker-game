using TMPro;
using UnityEngine;


[CreateAssetMenu(menuName = "NPC/Enemy")]
public class EnemyItem : BaseItem
{
    public int maxBaseHealth = 100;
    public int baseAttackPower = 10;
    public float baseAttackSpeed = 1f;

    public int xp = 10;
    public int gold = 10;

    public Sprite largeSprite;
}

