using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<HeroItem> heroes = new List<HeroItem>();

    public List<WeaponItem> weapons = new List<WeaponItem>();

    public List<HelmetItem> helmets = new List<HelmetItem>();
    public List<ChestplateItem> chestplates = new List<ChestplateItem>();
    public List<LeggingsItem> leggings = new List<LeggingsItem>();
    public List<BootsItem> boots = new List<BootsItem>();

    public event Action onItemPurchased;

    public Inventory inventory;

    public void PurchaseItem(BaseItem item, ItemTypes type, GameObject itemFrame)
    {
        if (inventory.gold >= item.price)
        {
            inventory.gold -= item.price;

            switch (type)
            {
                case ItemTypes.Hero:
                    inventory.heroes.Add(item as HeroItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Weapon:
                    inventory.weapons.Add(item as WeaponItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Helmet:
                    inventory.helmets.Add(item as HelmetItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Chestplate:
                    inventory.chestplates.Add(item as ChestplateItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Leggings:
                    inventory.leggings.Add(item as LeggingsItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
                case ItemTypes.Boots:
                    inventory.boots.Add(item as BootsItem);
                    item.SetUIItemAsOwned(itemFrame);
                    break;
            }

            if (onItemPurchased != null) onItemPurchased();
        }
    }



}
