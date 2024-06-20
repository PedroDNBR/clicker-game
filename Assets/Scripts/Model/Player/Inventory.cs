using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gold;

    public static Inventory instance;

    public event Action onGoldChange;

    public EnemySpawner enemySpawner;

    public List<HeroItem> heroes = new List<HeroItem>();

    public List<WeaponItem> weapons = new List<WeaponItem>();

    public List<HelmetItem> helmets = new List<HelmetItem>();
    public List<ChestplateItem> chestplates = new List<ChestplateItem>();
    public List<LeggingsItem> leggings = new List<LeggingsItem>();
    public List<BootsItem> boots = new List<BootsItem>();

    public HeroItem heroInField;

    public HeroController heroController;

    public Inventory()
    {
        instance = this;
    }

    public void Init()
    {
        enemySpawner.OnEnemySpawn += SubscribeToEnemyDestroyedAction;
    }

    void SubscribeToEnemyDestroyedAction(Enemy enemy)
    {
        enemy.onDestroyedAction += AddEnemyGold;
    }

    public void AddEnemyGold(Enemy enemy)
    {
        AddGold(enemy.gold);
    }

    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
        if (onGoldChange != null) onGoldChange();
    }

    public void SetHeroInField(HeroItem hero)
    {
        heroInField = hero;
        heroController.SpawnHero(hero);
    }

    public HeroItem GetHeroInField()
    {
        return heroInField;
    }

    public int GetGold()
    {
        return gold;
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
}
