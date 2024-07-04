using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    public interface IShop
    {
        /*
         * Shop dictionary <LocalID, ItemDatabaseId>
         */
        public event Action onItemPurchased;

        public void PurchaseItem(string id, IBaseItem item, ItemTypes type, GameObject itemFrame);

        void BuyItemAndSetAsOwned(string item, GameObject itemFrame, Dictionary<string, InventoryItem> dictionary);

        IEnumerator SetToSellAgain(IBaseItem item, GameObject itemFrame);
    }
}
