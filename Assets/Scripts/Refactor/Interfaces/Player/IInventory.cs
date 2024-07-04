using System.Collections.Generic;
using System;
using UnityEngine;

namespace RC
{
    public interface IInventory
    {
        public event Action onGoldChange;

        public event Action<string, bool, ItemTypes, GameObject> onHeroItemEquipped;
        public event Action<ItemTypes> onHeroItemUnequip;

        public Dictionary<string, InventoryItem> HeroesId { get; set; }
        public Dictionary<string, InventoryItem> WeaponsId { get; set; }
        public Dictionary<string, InventoryItem> HelmetsId { get; set; }
        public Dictionary<string, InventoryItem> ChestplatesId { get; set; }
        public Dictionary<string, InventoryItem> LeggingsId { get; set; }
        public Dictionary<string, InventoryItem> BootsId { get; set; }

        int Gold { get; set; }

        HeroItem SelectedHero { get; set; }

        ItemTypes SelectedItemType { get; set; }

        public void Init();

        public HeroItem GetHeroInField();

        public void SubscribeToEnemyDestroyedAction(ICharacter enemy);

        public void AddEnemyGold(ICharacter enemy);

        public string GetBootsGlobalIdByLocalId(string id);

        public string GetChestplateGlobalIdByLocalId(string id);

        public string GetHelmetGlobalIdByLocalId(string id);

        public string GetHeroGlobalIdByLocalId(string id);

        public string GetLeggingsGlobalIdByLocalId(string id);

        public string GetWeaponGlobalIdByLocalId(string id);

        public ArmorItem GetBootsItemByLocalId(string id);

        public ArmorItem GetChestplateItemByLocalId(string id);

        public ArmorItem GetHelmetItemByLocalId(string id);

        public ArmorItem GetLeggingsItemByLocalId(string id);

        public HeroItem GetHeroItemByLocalId(string id);

        public WeaponItem GetWeaponItemByLocalId(string id);

        public void UnequipSelectedItemType();

        public void EquipInHero(string localItemId, string globalItemId, ItemTypes type, GameObject itemFrame);

        public void AddGold(int goldToAdd);
    }
}
