using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    public class Shop : MonoBehaviour, IShop
    {
        public static Shop instance;

        public event Action onItemPurchased;

        public Shop()
        {
            instance = this;
        }

        public void BuyItemAndSetAsOwned(string itemId, GameObject itemFrame, Dictionary<string, InventoryItem> dictionary)
        {
            dictionary.Add("local_" + System.Guid.NewGuid().ToString(), new InventoryItem(itemId));

            IBaseItem item = ItemDatabase.instance.GetItemById(itemId);

            item.SetUIItemAsOwned(itemFrame);
            StartCoroutine(SetToSellAgain(item, itemFrame));
        }

        public void PurchaseItem(string id, IBaseItem item, ItemTypes type, GameObject itemFrame)
        {
            if (Inventory.instance.Gold < item.Price) return;

            Inventory.instance.Gold = Inventory.instance.Gold - item.Price;

            switch (type)
            {
                case ItemTypes.Hero:
                    IHeroItem heroItem = (IHeroItem)item;
                    heroItem.Owned = true;
                    Inventory.instance.HeroesId.Add("local_"+System.Guid.NewGuid().ToString(), new InventoryItem(id));
                    heroItem.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Weapon:
                    BuyItemAndSetAsOwned(id, itemFrame, Inventory.instance.WeaponsId);
                    break;
                case ItemTypes.Helmet:
                    BuyItemAndSetAsOwned(id, itemFrame, Inventory.instance.HelmetsId);
                    break;
                case ItemTypes.Chestplate:
                    BuyItemAndSetAsOwned(id, itemFrame, Inventory.instance.ChestplatesId);
                    break;
                case ItemTypes.Leggings:
                    BuyItemAndSetAsOwned(id, itemFrame, Inventory.instance.LeggingsId);
                    break;
                case ItemTypes.Boots:
                    BuyItemAndSetAsOwned(id, itemFrame, Inventory.instance.BootsId);
                    break;
            }

            if (onItemPurchased != null) onItemPurchased();
        }

        public IEnumerator SetToSellAgain(IBaseItem item, GameObject itemFrame)
        {
            yield return new WaitForSeconds(1f);
            if (itemFrame && itemFrame.activeSelf == true)
            {
                item.SetUIForShop(itemFrame);
                item.EnableButton(itemFrame);
            }
        }
    }
}
