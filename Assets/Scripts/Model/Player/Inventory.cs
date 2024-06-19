using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gold;

    public event Action onGoldChange;

    public EnemySpawner enemySpawner;

    public List<HeroItem> heroes = new List<HeroItem>();

    public List<WeaponItem> weapons = new List<WeaponItem>();

    public List<HelmetItem> helmets = new List<HelmetItem>();
    public List<ChestplateItem> chestplates = new List<ChestplateItem>();
    public List<LeggingsItem> leggings = new List<LeggingsItem>();
    public List<BootsItem> boots = new List<BootsItem>();

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

    public int GetGold()
    {
        return gold;
    }
}
