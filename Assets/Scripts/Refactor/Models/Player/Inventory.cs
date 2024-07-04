using System;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    public class Inventory : MonoBehaviour, IInventory
    {
        public static Inventory instance;

        // Dictionary<LocalId, InventoryItem(GlobalId, isEquipped)>
        Dictionary<string, InventoryItem> heroesId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> HeroesId { get => heroesId; set => heroesId = value; }

        Dictionary<string, InventoryItem> weaponsId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> WeaponsId { get => weaponsId; set => weaponsId = value; }

        Dictionary<string, InventoryItem> helmetsId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> HelmetsId { get => helmetsId; set => helmetsId = value; }

        Dictionary<string, InventoryItem> chestplatesId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> ChestplatesId { get => chestplatesId; set => chestplatesId = value; }

        Dictionary<string, InventoryItem> leggingsId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> LeggingsId { get => leggingsId; set => leggingsId = value; }

        Dictionary<string, InventoryItem> bootsId = new Dictionary<string, InventoryItem>();
        public Dictionary<string, InventoryItem> BootsId { get => bootsId; set => bootsId = value; }

        int gold = 0;

        public int Gold {
            get => gold;
            set
            {
                gold = value;
                if (onGoldChange != null) onGoldChange();
            } 
        }
        HeroItem heroInField = null;
        public HeroItem SelectedHero { get; set; }
        public ItemTypes SelectedItemType { get; set; }

        public event Action onGoldChange;
        public event Action<string, bool, ItemTypes, GameObject> onHeroItemEquipped;
        public event Action<ItemTypes> onHeroItemUnequip;

        public Inventory()
        {
            instance = this;
        }

        public HeroItem GetHeroInField()
        {
            return heroInField;
        }

        public void SetHeroInField(HeroItem hero, string localId = "")
        {
            heroInField = hero;
            if (hero != null)
            {
                HeroController.instance.SpawnHero(hero, localId);
            }
        }

        public void Init()
        {
            EnemyController.instance.OnEnemySpawn += SubscribeToEnemyDestroyedAction;
        }

        public void SubscribeToEnemyDestroyedAction(ICharacter enemy)
        {
            enemy.onDestroyedAction += AddEnemyGold;
        }

        public void AddEnemyGold(ICharacter enemy)
        {
            AddGold((enemy.CharacterItem as IEnemyItem).Gold);
        }

        public void EquipInHero(string localItemId, string globalItemId, ItemTypes type, GameObject itemFrame)
        {
            if (SelectedHero == null) return;
            switch (type)
            {
                case ItemTypes.Weapon:
                    if (!string.IsNullOrEmpty(SelectedHero.WeaponId)) WeaponsId[localItemId].isEquipped = false;
                    SelectedHero.WeaponId = localItemId;
                    WeaponsId[localItemId].isEquipped = true;
                    break;
                case ItemTypes.Helmet:
                    if (!string.IsNullOrEmpty(SelectedHero.HelmetId)) HelmetsId[localItemId].isEquipped = false;
                    SelectedHero.HelmetId = localItemId;
                    HelmetsId[localItemId].isEquipped = true;
                    break;
                case ItemTypes.Chestplate:
                    if (!string.IsNullOrEmpty(SelectedHero.ChestplateId)) ChestplatesId[localItemId].isEquipped = false;
                    SelectedHero.ChestplateId = localItemId;
                    ChestplatesId[localItemId].isEquipped = true;
                    break;
                case ItemTypes.Leggings:
                    if (!string.IsNullOrEmpty(SelectedHero.LeggingsId)) LeggingsId[localItemId].isEquipped = false;
                    SelectedHero.LeggingsId = localItemId;
                    LeggingsId[localItemId].isEquipped = true;
                    break;
                case ItemTypes.Boots:
                    if (!string.IsNullOrEmpty(SelectedHero.BootsId)) BootsId[localItemId].isEquipped = false;
                    SelectedHero.BootsId = localItemId;
                    BootsId[localItemId].isEquipped = true;
                    break;
            }

            if (onHeroItemEquipped != null) onHeroItemEquipped(globalItemId, true, type, itemFrame);
        }

        public string GetBootsGlobalIdByLocalId(string id)
        {
            return BootsId[id].globalId;
        }

        public string GetChestplateGlobalIdByLocalId(string id)
        {
            return ChestplatesId[id].globalId;
        }

        public string GetHelmetGlobalIdByLocalId(string id)
        {
            return HelmetsId[id].globalId;
        }

        public string GetHeroGlobalIdByLocalId(string id)
        {
            return HeroesId[id].globalId;
        }

        public string GetLeggingsGlobalIdByLocalId(string id)
        {
            return LeggingsId[id].globalId;
        }

        public string GetWeaponGlobalIdByLocalId(string id)
        {
            return WeaponsId[id].globalId;
        }

        public void UnequipSelectedItemType()
        {
            if (SelectedItemType == ItemTypes.None) return;

            switch (SelectedItemType)
            {
                case ItemTypes.Weapon:
                    if (!string.IsNullOrEmpty(SelectedHero.WeaponId)) WeaponsId[SelectedHero.WeaponId].isEquipped = false;
                    SelectedHero.WeaponId = string.Empty;
                    break;
                case ItemTypes.Helmet:
                    if (!string.IsNullOrEmpty(SelectedHero.HelmetId)) HelmetsId[SelectedHero.HelmetId].isEquipped = false;
                    SelectedHero.HelmetId = string.Empty;
                    break;
                case ItemTypes.Chestplate:
                    if (!string.IsNullOrEmpty(SelectedHero.ChestplateId)) ChestplatesId[SelectedHero.ChestplateId].isEquipped = false;
                    SelectedHero.ChestplateId = string.Empty;
                    break;
                case ItemTypes.Leggings:
                    if (!string.IsNullOrEmpty(SelectedHero.LeggingsId)) LeggingsId[SelectedHero.LeggingsId].isEquipped = false;
                    SelectedHero.LeggingsId = string.Empty;
                    break;
                case ItemTypes.Boots:
                    if (!string.IsNullOrEmpty(SelectedHero.BootsId)) BootsId[SelectedHero.BootsId].isEquipped = false;
                    SelectedHero.BootsId = string.Empty;
                    break;
            }

            if (onHeroItemUnequip != null) onHeroItemUnequip(SelectedItemType);
        }

        public void AddGold(int goldToAdd)
        {
            gold += goldToAdd;
            if (onGoldChange != null) onGoldChange();
        }

        public ArmorItem GetBootsItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(BootsId[id].globalId) as ArmorItem;
        }

        public ArmorItem GetChestplateItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(ChestplatesId[id].globalId) as ArmorItem;
        }

        public ArmorItem GetHelmetItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(HelmetsId[id].globalId) as ArmorItem;
        }

        public ArmorItem GetLeggingsItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(LeggingsId[id].globalId) as ArmorItem;
        }

        public HeroItem GetHeroItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(HeroesId[id].globalId) as HeroItem;
        }

        public WeaponItem GetWeaponItemByLocalId(string id)
        {
            return ItemDatabase.instance.GetItemById(WeaponsId[id].globalId) as WeaponItem;
        }
    }

    public class InventoryItem
    {
        public string globalId;
        public bool isEquipped = false;

        public InventoryItem(string globalId)
        {
            this.globalId = globalId;
        }
    }
}
