using System;
using System.Collections;
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

    public void PurchaseHero(HeroItem hero, GameObject itemFrame)
    {
        if (inventory.gold >= hero.price)
        {
            inventory.gold -= hero.price;
            inventory.heroes.Add(hero);

            itemFrame.GetComponentsInChildren<TMP_Text>()[0].text = $"{hero.name}\nPurchased";
            itemFrame.GetComponent<Button>().interactable = false;

            if (onItemPurchased != null) onItemPurchased();
        }
    }
}
