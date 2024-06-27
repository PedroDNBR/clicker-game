using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPersistanceController : MonoBehaviour
{
    [Header("File Storage")]
    [SerializeField] string fileName;

    GameData gameData;

    public static DataPersistanceController instance;

    private FileDataController fileDataController;

    public Image test;

    public DataPersistanceController()
    {
        instance = this;
    }

    private void Start()
    {
        fileDataController = new FileDataController(Application.persistentDataPath, fileName);
        LoadGame();
        StartCoroutine(SaveEveryThirtySeconds());
    }

    IEnumerator SaveEveryThirtySeconds()
    {
        yield return new WaitForSeconds(30);
        SaveGame();
        StartCoroutine(SaveEveryThirtySeconds());
    }

    public void SaveGame()
    {
        GameData newGameData = new GameData();

        newGameData.heroes = HeroesItemListToHeroesItemSaveList(Inventory.instance.heroes);
        newGameData.weapons = WeaponsItemListToWeaponsItemSaveList(Inventory.instance.weapons);

        List<ArmorItem> helmets = new List<ArmorItem>(Inventory.instance.helmets);
        newGameData.helmets = ArmorsItemListToArmorsItemSaveList(helmets);

        List<ArmorItem> chestplates = new List<ArmorItem>(Inventory.instance.chestplates);
        newGameData.chestplates = ArmorsItemListToArmorsItemSaveList(chestplates);

        List<ArmorItem> leggings = new List<ArmorItem>(Inventory.instance.leggings);
        newGameData.leggings = ArmorsItemListToArmorsItemSaveList(leggings);

        List<ArmorItem> boots = new List<ArmorItem>(Inventory.instance.boots);
        newGameData.boots = ArmorsItemListToArmorsItemSaveList(boots);

        if(Inventory.instance.GetHeroInField() != null)
        {
            newGameData.heroInField = HeroItemToHeroItemSave(Inventory.instance.GetHeroInField());
        }
        else
        {
            newGameData.heroInField = null;
        }

        newGameData.gold = Inventory.instance.GetGold();

        newGameData.xp = Level.instance.GetXp();

        if(EnemySpawner.instance.GetSpawnedEnemy())
        {
            newGameData.enemy = EnemyItemToEnemyItemSave(EnemySpawner.instance.GetSpawnedEnemy().GetEnemyItem());
        }
        else
        {
            newGameData.enemy = null;
        }

        newGameData.enemiesPointsMultiplier = EnemySpawner.instance.GetEnemiesPointsMultiplier();

        newGameData.shoppedItemCount = Shop.instance.shoppedItemCount;

        gameData = newGameData;
        fileDataController.Save(gameData);
    }

    public void LoadGame()
    {
        gameData = fileDataController.Load();
        if (gameData == null)
        {
            SaveGame();
            return;
        }
        else
        {
            Inventory.instance.heroes = HeroesItemSaveListToHeroesItemList(gameData.heroes);
            Inventory.instance.weapons = WeaponsItemSaveListToWeaponsItemList(gameData.weapons);
            Inventory.instance.helmets = HelmetsItemSaveListToHelmetsItemList(gameData.helmets);
            Inventory.instance.chestplates = ChestplatesItemSaveListToChestplatesItemList(gameData.chestplates);
            Inventory.instance.leggings = LeggingsItemSaveListToLeggingsItemList(gameData.leggings);
            Inventory.instance.boots = BootsItemSaveListToBootsItemList(gameData.boots);

            if (gameData.heroInField.maxBaseHealth > 0 && gameData.heroInField.baseAttackPower > 0 && gameData.heroInField.itemName != "" )
            {
                Inventory.instance.SetHeroInField(HeroItemSaveToHeroItem(gameData.heroInField));
            }

            if (gameData.enemy.maxBaseHealth > 0 && gameData.enemy.baseAttackPower > 0)
            {
                EnemySpawner.instance.SpawnEnemyFromSave(EnemyItemSaveToEnemyItem(gameData.enemy));
            }

            EnemySpawner.instance.SetEnemiesPointsMultiplier(gameData.enemiesPointsMultiplier);

            Shop.instance.shoppedItemCount = gameData.shoppedItemCount;


            Inventory.instance.SetGold(gameData.gold);
            Level.instance.SetXp(gameData.xp);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void CloseEvent()
    {
        SaveGame();
        test.color = Color.red;
    }

    /*
     * 
     * Convert Regular Items to Save Items
     * 
     */
    List<HeroItemSave> HeroesItemListToHeroesItemSaveList(List<HeroItem> heroes)
    {
        List<HeroItemSave> heroItemSaveList = new List<HeroItemSave>();
        foreach (HeroItem hero in heroes)
        {
            heroItemSaveList.Add(HeroItemToHeroItemSave(hero));
        }
        return heroItemSaveList;
    }

    List<WeaponItemSave> WeaponsItemListToWeaponsItemSaveList(List<WeaponItem> weapons)
    {
        List<WeaponItemSave> weaponItemSaveList = new List<WeaponItemSave>();
        foreach (WeaponItem weapon in weapons)
        {
            weaponItemSaveList.Add(WeaponItemToWeaponItemSave(weapon));
        }
        return weaponItemSaveList;
    }

    List<ArmorItemSave> ArmorsItemListToArmorsItemSaveList(List<ArmorItem> armors)
    {
        List<ArmorItemSave> armorItemSaveList = new List<ArmorItemSave>();
        foreach (ArmorItem armor in armors)
        {
            if(armor != null) armorItemSaveList.Add(ArmorItemToArmorItemSave(armor));
        }
        return armorItemSaveList;
    }

    HeroItemSave HeroItemToHeroItemSave(HeroItem hero)
    {
        HeroItemSave heroItemSave = new HeroItemSave();

        SetBaseItemSaveData(hero, heroItemSave);

        heroItemSave.maxBaseHealth = hero.maxBaseHealth;
        heroItemSave.baseAttackPower = hero.baseAttackPower;
        heroItemSave.baseAttackSpeed = hero.baseAttackSpeed;

        heroItemSave.largeSprite = hero.largeSprite;

        heroItemSave.healthWhenInField = hero.GetHealthWhenInField();

        heroItemSave.shopIndex = hero.GetShopIndex();

        if (hero.weapon != null) heroItemSave.weapon = WeaponItemToWeaponItemSave(hero.weapon);

        if (hero.helmet != null) heroItemSave.helmet = ArmorItemToArmorItemSave(hero.helmet);
        if (hero.chestplate != null) heroItemSave.chestplate = ArmorItemToArmorItemSave(hero.chestplate);
        if (hero.leggings != null) heroItemSave.leggings = ArmorItemToArmorItemSave(hero.leggings);
        if (hero.boots != null) heroItemSave.boots = ArmorItemToArmorItemSave(hero.boots);

        heroItemSave.owned = hero.GetOwned();

        return heroItemSave;
    }

    private void SetBaseItemSaveData(BaseItem baseItem, BaseItemSave baseItemSave)
    {
        baseItemSave.itemName = baseItem.itemName;

        baseItemSave.sprite = baseItem.sprite;

        baseItemSave.smallSprite = baseItem.smallSprite;

        baseItemSave.price = baseItem.price;

        baseItemSave.equipped = baseItem.GetEquipped();

        baseItemSave.minLevelToUnlock = baseItem.minLevelToUnlock;

        baseItemSave.shoppedItemCount = baseItem.shoppedItemCount;
    }

    WeaponItemSave WeaponItemToWeaponItemSave(WeaponItem weapon)
    {
        WeaponItemSave weaponItemSave = new WeaponItemSave();

        SetBaseItemSaveData(weapon, weaponItemSave);

        weaponItemSave.attackPowerBoost = weapon.attackPowerBoost;
        weaponItemSave.attackSpeedBoost = weapon.attackSpeedBoost;

        return weaponItemSave;
    }

    ArmorItemSave ArmorItemToArmorItemSave(ArmorItem armor)
    {
        ArmorItemSave armorItemSave = new ArmorItemSave();

        SetBaseItemSaveData(armor, armorItemSave);

        armorItemSave.armorPoints = armor.armorPoints;

        return armorItemSave;
    }

    EnemyItemSave EnemyItemToEnemyItemSave(EnemyItem enemy)
    {
        EnemyItemSave enemyItemSave = new EnemyItemSave();

        SetBaseItemSaveData(enemy, enemyItemSave);

        enemyItemSave.maxBaseHealth = enemy.maxBaseHealth;
        enemyItemSave.baseAttackPower  = enemy.baseAttackPower;
        enemyItemSave.baseAttackSpeed  = enemy.baseAttackSpeed;

        enemyItemSave.xp  = enemy.xp;
        enemyItemSave.gold  = enemy.gold;

        enemyItemSave.largeSpriteIndex = enemy.largeSpriteIndex;


        return enemyItemSave;
    }

    /*
     * 
     * Convert Save Items to Regular Items
     * 
     */
    List<HeroItem> HeroesItemSaveListToHeroesItemList(List<HeroItemSave> heroes)
    {
        List<HeroItem> heroItemSaveList = new List<HeroItem>();
        foreach (HeroItemSave hero in heroes)
        {
            heroItemSaveList.Add(HeroItemSaveToHeroItem(hero));
            Shop.instance.heroes[hero.shopIndex].SetOwned(true);
        }
        return heroItemSaveList;
    }

    List<WeaponItem> WeaponsItemSaveListToWeaponsItemList(List<WeaponItemSave> weapons)
    {
        List<WeaponItem> weaponItemSaveList = new List<WeaponItem>();
        foreach (WeaponItemSave weapon in weapons)
        {
            weaponItemSaveList.Add(WeaponItemSaveToWeaponItem(weapon));
        }
        return weaponItemSaveList;
    }

    List<HelmetItem> HelmetsItemSaveListToHelmetsItemList(List<ArmorItemSave> armors)
    {
        List<HelmetItem> armorItemList = new List<HelmetItem>();
        foreach (ArmorItemSave armor in armors)
        {
            armorItemList.Add(ArmorItemSaveToHelmetItem(armor));
        }
        return armorItemList;
    }

    List<ChestplateItem> ChestplatesItemSaveListToChestplatesItemList(List<ArmorItemSave> armors)
    {
        List<ChestplateItem> armorItemList = new List<ChestplateItem>();
        foreach (ArmorItemSave armor in armors)
        {
            armorItemList.Add(ArmorItemSaveToChestplateItem(armor));
        }
        return armorItemList;
    }

    List<LeggingsItem> LeggingsItemSaveListToLeggingsItemList(List<ArmorItemSave> armors)
    {
        List<LeggingsItem> armorItemList = new List<LeggingsItem>();
        foreach (ArmorItemSave armor in armors)
        {
            armorItemList.Add(ArmorItemSaveToLeggingsItem(armor));
        }
        return armorItemList;
    }

    List<BootsItem> BootsItemSaveListToBootsItemList(List<ArmorItemSave> armors)
    {
        List<BootsItem> armorItemList = new List<BootsItem>();
        foreach (ArmorItemSave armor in armors)
        {
            armorItemList.Add(ArmorItemSaveToBootsItem(armor));
        }
        return armorItemList;
    }

    HeroItem HeroItemSaveToHeroItem(HeroItemSave hero)
    {
        // HeroItem heroItem = new HeroItem();

        HeroItem heroItem = (HeroItem)ScriptableObject.CreateInstance(typeof(HeroItem));

        SetBaseItemData(hero, heroItem);

        heroItem.maxBaseHealth = hero.maxBaseHealth;
        heroItem.baseAttackPower = hero.baseAttackPower;
        heroItem.baseAttackSpeed = hero.baseAttackSpeed;

        heroItem.largeSprite = hero.largeSprite;

        heroItem.minLevelToUnlock = hero.minLevelToUnlock;

        heroItem.SetHealthWhenInField(hero.healthWhenInField);

        heroItem.SetShopIndex(hero.shopIndex);

        if (hero.weapon.price > 0 && hero.weapon.itemName != "") heroItem.weapon = WeaponItemSaveToWeaponItem(hero.weapon);

        if (hero.helmet.price > 0 && hero.helmet.itemName != "") heroItem.helmet = HelmetItemSaveToHelmetItem(hero.helmet);
        if (hero.chestplate.price > 0 && hero.chestplate.itemName != "") heroItem.chestplate = ChestplateItemSaveToChestplateItem(hero.chestplate);
        if (hero.leggings.price > 0 && hero.leggings.itemName != "") heroItem.leggings = LeggingsItemSaveToLeggingsItem(hero.leggings);
        if (hero.boots.price > 0 && hero.boots.itemName != "") heroItem.boots = BootsItemSaveToBootsItem(hero.boots);

        heroItem.SetOwned(hero.owned);

        return heroItem;
    }

    private void SetBaseItemData(BaseItemSave baseItemSave, BaseItem baseItem)
    {
        baseItem.itemName = baseItemSave.itemName;

        baseItem.sprite = baseItemSave.sprite;

        baseItem.smallSprite = baseItemSave.smallSprite;

        baseItem.price = baseItemSave.price;

        baseItem.shoppedItemCount = baseItemSave.shoppedItemCount;

        baseItem.SetEquipped(baseItemSave.equipped);
    }

    HelmetItem ArmorItemSaveToHelmetItem(ArmorItemSave armor)
    {
        HelmetItem armorItem = (HelmetItem)ScriptableObject.CreateInstance(typeof(HelmetItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    ChestplateItem ArmorItemSaveToChestplateItem(ArmorItemSave armor)
    {
        ChestplateItem armorItem = (ChestplateItem)ScriptableObject.CreateInstance(typeof(ChestplateItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    LeggingsItem ArmorItemSaveToLeggingsItem(ArmorItemSave armor)
    {
        LeggingsItem armorItem = (LeggingsItem)ScriptableObject.CreateInstance(typeof(LeggingsItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    BootsItem ArmorItemSaveToBootsItem(ArmorItemSave armor)
    {
        BootsItem armorItem = (BootsItem)ScriptableObject.CreateInstance(typeof(BootsItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    WeaponItem WeaponItemSaveToWeaponItem(WeaponItemSave weapon)
    {
        WeaponItem weaponItem = (WeaponItem)ScriptableObject.CreateInstance(typeof(WeaponItem));

        SetBaseItemData(weapon, weaponItem);

        weaponItem.attackPowerBoost = weapon.attackPowerBoost;
        weaponItem.attackSpeedBoost = weapon.attackSpeedBoost;

        return weaponItem;
    }

    HelmetItem HelmetItemSaveToHelmetItem(ArmorItemSave armor)
    {
        HelmetItem armorItem = (HelmetItem)ScriptableObject.CreateInstance(typeof(HelmetItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    ChestplateItem ChestplateItemSaveToChestplateItem(ArmorItemSave armor)
    {
        ChestplateItem armorItem = (ChestplateItem)ScriptableObject.CreateInstance(typeof(ChestplateItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    LeggingsItem LeggingsItemSaveToLeggingsItem(ArmorItemSave armor)
    {
        LeggingsItem armorItem = (LeggingsItem)ScriptableObject.CreateInstance(typeof(LeggingsItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    BootsItem BootsItemSaveToBootsItem(ArmorItemSave armor)
    {
        BootsItem armorItem = (BootsItem)ScriptableObject.CreateInstance(typeof(BootsItem));
        SetBaseItemData(armor, armorItem);
        armorItem.armorPoints = armor.armorPoints;

        return armorItem;
    }

    EnemyItem EnemyItemSaveToEnemyItem(EnemyItemSave enemy)
    {
        EnemyItem enemyItem = new EnemyItem();

        SetBaseItemData(enemy, enemyItem);

        enemyItem.largeSpriteIndex = enemy.largeSpriteIndex;

        enemyItem.maxBaseHealth = enemy.maxBaseHealth;
        enemyItem.baseAttackPower = enemy.baseAttackPower;
        enemyItem.baseAttackSpeed = enemy.baseAttackSpeed;

        enemyItem.spritePath = enemy.spritePath;

        enemyItem.xp = enemy.xp;
        enemyItem.gold = enemy.gold;

        return enemyItem;
    }
}
