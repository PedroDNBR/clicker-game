using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int gold = 0;

    public int xp = 0;

    public HeroItemSave heroInField;

    public List<HeroItemSave> heroes = new List<HeroItemSave>();

    public List<WeaponItemSave> weapons = new List<WeaponItemSave>();

    public List<ArmorItemSave> helmets = new List<ArmorItemSave>();
    public List<ArmorItemSave> chestplates = new List<ArmorItemSave>();
    public List<ArmorItemSave> leggings = new List<ArmorItemSave>();
    public List<ArmorItemSave> boots = new List<ArmorItemSave>();

    public EnemyItemSave enemy;

    public Sprite enemyLargeSprite;

    public float enemiesPointsMultiplier = 1f;

    public int shoppedItemCount = -1;

    public GameData()
    {

    }
}

[System.Serializable]
public class BaseItemSave
{
    public string itemName;

    public Sprite sprite;

    public Sprite smallSprite;

    public int price;

    public int minLevelToUnlock;
    
    public bool equipped;

    public int shoppedItemCount = -1;
}

[System.Serializable]
public class HeroItemSave : BaseItemSave
{
    public int maxBaseHealth;
    public int baseAttackPower;
    public float baseAttackSpeed;

    public Sprite largeSprite;

    public int healthWhenInField;

    public int shopIndex = -1;

    public WeaponItemSave weapon;

    public ArmorItemSave helmet;
    public ArmorItemSave chestplate;
    public ArmorItemSave leggings;
    public ArmorItemSave boots;

    public bool owned;
}

[System.Serializable]
public class WeaponItemSave : BaseItemSave
{
    public int attackPowerBoost;
    public float attackSpeedBoost;
}

[System.Serializable]
public class ArmorItemSave : BaseItemSave
{
    public int armorPoints;
}

[System.Serializable]
public class EnemyItemSave : BaseItemSave
{
    public int maxBaseHealth;
    public int baseAttackPower;
    public float baseAttackSpeed;

    public string spritePath;

    public int xp;
    public int gold;



    public int largeSpriteIndex;
}
