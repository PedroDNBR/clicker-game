using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewShop : MonoBehaviour
{
    public GameObject itemObject;
    public VerticalLayoutGroup itemGrid;

    public GameObject shopItem;

    public Inventory inventory;
    public Shop shop;

    private void Start()
    {
        CleanItemsFromGrid();
        FillShopWithHeroes();
    }

    public void ToggleShopWindow()
    {
        shopItem.SetActive(!shopItem.activeSelf);
        if(shopItem.activeSelf) OpenHeroesShop();
    }

    public void ShowShopWindow()
    {
        shopItem.SetActive(true);
        OpenHeroesShop();
    }

    public void HideShopWindow()
    {
        shopItem.SetActive(false);
    }

    public void OpenHeroesShop()
    {
        CleanItemsFromGrid();
        FillShopWithHeroes();
    }

    public void OpenWeaponsShop()
    {
        CleanItemsFromGrid();
        FillShopWithWeapons();
    }

    public void OpenArmorsShop()
    {
        CleanItemsFromGrid();
        FillShopWithArmmors();
    }

    void CleanItemsFromGrid()
    {
        foreach (Transform child in itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateHeroInUI(HeroItem hero)
    {
        GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);

        hero.SetUIItemSprite(ItemInMenu);

        if (hero.GetOwned())
        {
            hero.SetUIItemNameAndOwned(ItemInMenu);
            hero.DisableButton(ItemInMenu);
        }
        else
        {
            hero.SetUIItemNameAndPrice(ItemInMenu);
        }

        hero.SetUIIItemStats(ItemInMenu);

        ItemInMenu.GetComponent<Button>().onClick.AddListener(() => shop.PurchaseItem(hero, ItemTypes.Hero, ItemInMenu));

        ItemInMenu.SetActive(true);
    }

    void FillShopWithHeroes()
    {
        foreach (HeroItem hero in shop.heroes)
        {
            if(Level.instance.GetLevel() >= hero.minLevelToUnlock)
            {
                InstantiateHeroInUI(hero);
            }
        }
    }

    void FillShopWithWeapons()
    {
        FillShopWithItems(new List<BaseItem>(shop.weapons), ItemTypes.Weapon);
    }

    void FillShopWithArmmors()
    {
        FillShopWithItems(new List<BaseItem>(shop.helmets), ItemTypes.Helmet);

        FillShopWithItems(new List<BaseItem>(shop.chestplates), ItemTypes.Chestplate);

        FillShopWithItems(new List<BaseItem>(shop.leggings), ItemTypes.Leggings);

        FillShopWithItems(new List<BaseItem>(shop.boots), ItemTypes.Boots);
    }

    void FillShopWithItems(List<BaseItem> itemList, ItemTypes ItemType)
    {
        foreach (BaseItem item in itemList)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            item.SetUIForShop(ItemInMenu);
            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => shop.PurchaseItem(item, ItemType, ItemInMenu));
            ItemInMenu.SetActive(true);
        }
    }
}
