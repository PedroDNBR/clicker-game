using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public List<HeroItem> heroes = new List<HeroItem>();

    public List<WeaponItem> weapons = new List<WeaponItem>();

    public List<HelmetItem> helmets = new List<HelmetItem>();
    public List<ChestplateItem> chestplates = new List<ChestplateItem>();
    public List<LeggingsItem> leggings = new List<LeggingsItem>();
    public List<BootsItem> boots = new List<BootsItem>();

    public event Action onItemPurchased;

    public Shop()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            heroes[i].SetOwned(false);
            heroes[i].SetShopIndex(i);
        }
    }

    public void PurchaseItem(BaseItem item, ItemTypes type, GameObject itemFrame)
    {
        if (Inventory.instance.gold >= item.price)
        {
            Inventory.instance.gold -= item.price;

            switch (type)
            {
                case ItemTypes.Hero:
                    (item as HeroItem).SetOwned(true);
                    Inventory.instance.heroes.Add(Instantiate(item) as HeroItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Weapon:
                    BuyItemAndSetAsOwned<WeaponItem>(item, itemFrame, Inventory.instance.weapons);
                    break;
                case ItemTypes.Helmet:
                    BuyItemAndSetAsOwned<HelmetItem>(item, itemFrame, Inventory.instance.helmets);
                    break;
                case ItemTypes.Chestplate:
                    BuyItemAndSetAsOwned<ChestplateItem>(item, itemFrame, Inventory.instance.chestplates);
                    break;
                case ItemTypes.Leggings:
                    BuyItemAndSetAsOwned<LeggingsItem>(item, itemFrame, Inventory.instance.leggings);
                    break;
                case ItemTypes.Boots:
                    BuyItemAndSetAsOwned<BootsItem>(item, itemFrame, Inventory.instance.boots);
                    break;
            }

            if (onItemPurchased != null) onItemPurchased();
        }
    }

    void BuyItemAndSetAsOwned<TBaseItem>(object item, GameObject itemFrame, List<TBaseItem> list)
    {
        if(item is TBaseItem)
        {
            TBaseItem test = (TBaseItem) item;
            list.Add(test);
            (item as BaseItem).SetUIItemAsOwned(itemFrame);
            StartCoroutine(SetToSellAgain((item as BaseItem), itemFrame));
        }
    }

    IEnumerator SetToSellAgain(BaseItem item, GameObject itemFrame)
    {
        yield return new WaitForSeconds(1f);
        if (itemFrame && itemFrame.activeSelf == true)
        {
            item.SetUIForShop(itemFrame);
            item.EnableButton(itemFrame);
        }
    }
}
