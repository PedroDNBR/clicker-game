using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class Testing
    {
    }

    public class ItemDictionary
    {
        public static ItemDictionary instance;
        
        Dictionary<string, HeroItem> heroes;

        Dictionary<string, ArmorItem> items;

        public Dictionary<string, HeroItem> GetHeroes() { return heroes; }

        public Dictionary<string, ArmorItem> GetItems() { return items; }

        public HeroItem FindHeroById(string id)
        {
            return heroes[id];
        }

        public ArmorItem FindItemById(string id)
        {
            return items[id];
        }
    }

    public class Shop
    {
        private void Start()
        {
            // Logic to get all items from ItemDictionary and print in the screen
        }

        public void PurchaseHero(string id)
        {
            Inventory.instance.AddItemToInventory(id);
        }

        public void PurchaseItem(string id)
        {
            Inventory.instance.AddItemToInventory(id);
        }
    }

    public class Inventory
    {
        public static Inventory instance;

        HeroInventory heroEquipped;

        int countHero = 0;
        Dictionary<int, string> heroIds;

        int countItem = 0;
        Dictionary<int, string> itemIds;

        public Dictionary<int, string> GetHeroIds() { return heroIds; }
        
        public Dictionary<int, string> GetItemIds() { return itemIds; }

        public string GetGlobalId(int localId)
        {
            return heroIds[localId];
        }

        public void AddHeroToInventory(string id)
        {
            heroIds.Add(countHero, id);
            countHero++;
        }

        public void AddItemToInventory(string id)
        {
            itemIds.Add(countItem, id);
            countItem++;
        }

        public void RemoteItemFromInventory(int id)
        {
            itemIds.Remove(id);
        }

        public void EquipHelmetInHero(int id)
        {
            heroEquipped.SetHelmet(id);
        }

        public void HeroDied()
        {

        }
    }

    public class HeroInventory
    {
        int helmetId = -1;

        public void SetHelmet(int helmetId)
        {
            this.helmetId = helmetId; 
        }

        public int GetTotalDamageToTake(int damage)
        {
            float totalDamage = damage;

            if (helmetId > -1) totalDamage -= totalDamage * (ItemDictionary.instance.FindItemById(Inventory.instance.GetGlobalId(helmetId))).armorPoints;

            return Mathf.CeilToInt(totalDamage);
        }
    }
}
