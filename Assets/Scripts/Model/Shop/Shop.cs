using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int shoppedItemCount = 0;

    public Shop()
    {
        instance = this;
    }

    private void Awake()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            heroes[i].SetOwned(false);
            heroes[i].SetShopIndex(i);
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetEquipped(false);
        }
        for (int i = 0; i < helmets.Count; i++)
        {
            helmets[i].SetEquipped(false);
        }
        for (int i = 0; i < chestplates.Count; i++)
        {
            chestplates[i].SetEquipped(false);
        }
        for (int i = 0; i < leggings.Count; i++)
        {
            leggings[i].SetEquipped(false);
        }
        for (int i = 0; i < boots.Count; i++)
        {
            boots[i].SetEquipped(false);
        }
    }

    public void PurchaseItem(BaseItem item, ItemTypes type, GameObject itemFrame)
    {
        if (Inventory.instance.GetGold() >= item.price)
        {
            Inventory.instance.SetGold(Inventory.instance.GetGold() - item.price);

            item.shoppedItemCount = shoppedItemCount;
            Debug.Log(shoppedItemCount);
            Debug.Log(item.shoppedItemCount);

            switch (type)
            {
                case ItemTypes.Hero:
                    HeroItem hero = (item as HeroItem);
                    hero.SetOwned(true);
                    hero.SetShopIndex((item as HeroItem).GetShopIndex());
                    HeroItem newHero = Instantiate(hero);
                    Inventory.instance.heroes.Add(newHero);
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
            shoppedItemCount++;

            if (onItemPurchased != null) onItemPurchased();
        }
    }

    void BuyItemAndSetAsOwned<TBaseItem>(BaseItem item, GameObject itemFrame, List<TBaseItem> list)
    {
        if (item is TBaseItem)
        {
            TBaseItem test = (TBaseItem)(object)Instantiate(item);
            list.Add(test);
            item.SetUIItemAsOwned(itemFrame);
            StartCoroutine(SetToSellAgain(item, itemFrame));
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
