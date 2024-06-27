using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public event Action onGoldChange;

    public event Action<BaseItem, ItemTypes, GameObject> onHeroItemEquipped;
    public event Action<ItemTypes> onHeroItemUnequip;

    [HideInInspector]
    public List<HeroItem> heroes = new List<HeroItem>();

    [HideInInspector]
    public List<WeaponItem> weapons = new List<WeaponItem>();

    [HideInInspector]
    public List<HelmetItem> helmets = new List<HelmetItem>();
    [HideInInspector]
    public List<ChestplateItem> chestplates = new List<ChestplateItem>();
    [HideInInspector]
    public List<LeggingsItem> leggings = new List<LeggingsItem>();
    [HideInInspector]
    public List<BootsItem> boots = new List<BootsItem>();

    int gold = 0;

    HeroItem heroInField;

    HeroItem selectedHero;
    ItemTypes selectedItemType = ItemTypes.None;

    public Inventory()
    {
        instance = this;
    }

    public void Init()
    {
        EnemySpawner.instance.OnEnemySpawn += SubscribeToEnemyDestroyedAction;
    }

    void SubscribeToEnemyDestroyedAction(Enemy enemy)
    {
        enemy.onDestroyedAction += AddEnemyGold;
    }

    public void UnequipSelectedItemType()
    {
        if (selectedItemType == ItemTypes.None) return;

        switch (selectedItemType)
        {
            case ItemTypes.Weapon:
                if (selectedHero.weapon != null) selectedHero.weapon.SetEquipped(false);
                selectedHero.weapon = null;
                break;
            case ItemTypes.Helmet:
                if (selectedHero.helmet != null) selectedHero.helmet.SetEquipped(false);
                selectedHero.helmet = null;
                break;
            case ItemTypes.Chestplate:
                if (selectedHero.chestplate != null) selectedHero.chestplate.SetEquipped(false);
                selectedHero.chestplate = null;
                break;
            case ItemTypes.Leggings:
                if (selectedHero.leggings != null) selectedHero.leggings.SetEquipped(false);
                selectedHero.leggings = null;
                break;
            case ItemTypes.Boots:
                if (selectedHero.boots != null) selectedHero.boots.SetEquipped(false);
                selectedHero.boots = null;
                break;
        }

        if (onHeroItemUnequip != null) onHeroItemUnequip(selectedItemType);

    }

    public void AddEnemyGold(Enemy enemy)
    {
        AddGold(enemy.GetEnemyItem().gold);
    }

    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
        if (onGoldChange != null) onGoldChange();
    }

    public void SetHeroInField(HeroItem hero)
    {
        heroInField = hero;
        if(hero != null)
        {
            HeroController.instance.SpawnHero(hero);
        }
    }

    public void EquipInHero(BaseItem item, ItemTypes type, GameObject itemFrame)
    {
        if (selectedHero == null) return;
        switch (type)
        {
            case ItemTypes.Weapon:
                if (selectedHero.weapon != null) selectedHero.weapon.SetEquipped(false);
                selectedHero.weapon = item as WeaponItem;
                break;
            case ItemTypes.Helmet:
                if (selectedHero.helmet != null) selectedHero.helmet.SetEquipped(false);
                selectedHero.helmet = item as HelmetItem;
                break;
            case ItemTypes.Chestplate:
                if (selectedHero.chestplate != null) selectedHero.chestplate.SetEquipped(false);
                selectedHero.chestplate = item as ChestplateItem;
                break;
            case ItemTypes.Leggings:
                if (selectedHero.leggings != null) selectedHero.leggings.SetEquipped(false);
                selectedHero.leggings = item as LeggingsItem;
                break;
            case ItemTypes.Boots:
                if (selectedHero.boots != null) selectedHero.boots.SetEquipped(false);
                selectedHero.boots = item as BootsItem;
                break;
        }

        item.SetEquipped(true);

        if (onHeroItemEquipped != null) onHeroItemEquipped(item, type, itemFrame);
    }

    public HeroItem GetHeroInField()
    {
        return heroInField;
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int gold)
    {
        this.gold = gold;
        if (onGoldChange != null) onGoldChange();
    }

    public HeroItem GetSelectedHero()
    {
        return selectedHero;
    }

    public void SetSelectedHero(HeroItem hero)
    {
        selectedHero = hero;
    }

    public ItemTypes GetSelectedItemType()
    {
        return selectedItemType;
    }

    public void SetSelectedItemType(ItemTypes type)
    {
        selectedItemType = type;
    }
}

public enum ItemTypes
{
    Hero,
    Weapon,
    Helmet,
    Chestplate,
    Leggings,
    Boots,
    None
}
