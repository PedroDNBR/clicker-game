using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RC
{
    public class ItemDatabase : MonoBehaviour, IItemDatabase
    {
        public static ItemDatabase instance;

        /*
         * Items dictionary <ID, ScriptableObject>
         */
        public Dictionary<string, IBaseItem> items = new Dictionary<string, IBaseItem>();
        public Dictionary<string, IBaseItem> Items { get => items; set => items = value; }

        /*
         * Items List
         */
        [SerializeField]
        public List<BaseItem> startingItemsList;

        public IBaseItem GetItemById(string id) { return Items[id]; }

        public ItemDatabase() { instance = this; }

        private void Awake()
        {
            PopulateItemDictionary();
            ClearStartingItems();
        }

        private void PopulateItemDictionary()
        {
            for (int i = 0; i < startingItemsList.Count; i++)
            {
                Items.Add("global_"+System.Guid.NewGuid().ToString(), startingItemsList[i]);
            }
        }

        private void ClearStartingItems()
        {
            for (int i = 0; i < Items.Count - 1; i++)
            {
                KeyValuePair<string, IBaseItem> itemPair = Items.ElementAt(i);
                IBaseItem Item = itemPair.Value;

                if (Item is IHeroItem)
                {
                    ((IHeroItem)Item).Owned = false;
                    ((IHeroItem)Item).WeaponId = string.Empty;
                    ((IHeroItem)Item).HelmetId = string.Empty;
                    ((IHeroItem)Item).ChestplateId = string.Empty;
                    ((IHeroItem)Item).LeggingsId = string.Empty;
                    ((IHeroItem)Item).BootsId = string.Empty;
                }
            }
        }

        public void ClearStartingItemsSave()
        {
            for (int i = 0; i < Items.Count - 1; i++)
            {
                KeyValuePair<string, IBaseItem> itemPair = Items.ElementAt(i);
                IBaseItem Item = itemPair.Value;

                if (Item is IHeroItem)
                {
                    ((IHeroItem)Item).WeaponId = string.Empty;
                    ((IHeroItem)Item).HelmetId = string.Empty;
                    ((IHeroItem)Item).ChestplateId = string.Empty;
                    ((IHeroItem)Item).LeggingsId = string.Empty;
                    ((IHeroItem)Item).BootsId = string.Empty;
                }
            }
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
}
