using TMPro;
using Unity.Android.Gradle;
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
    }

    public void ShowShopWindow()
    {
        shopItem.SetActive(true);
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

        if (inventory.heroes.Find(heroInArray => heroInArray == hero))
        {
            hero.SetUIItemNameAndOwned(ItemInMenu);
            hero.DisableButton(ItemInMenu);
        }
        else
        {
            hero.SetUIItemNameAndPrice(ItemInMenu);
        }

        hero.SetUIIHeroStats(ItemInMenu);

        ItemInMenu.GetComponent<Button>().onClick.AddListener(() => shop.PurchaseItem(hero, ItemTypes.Hero, ItemInMenu));

        ItemInMenu.SetActive(true);
    }

    void FillShopWithHeroes()
    {
        foreach (HeroItem hero in shop.heroes)
        {
            InstantiateHeroInUI(hero);
        }
    }

    void FillShopWithWeapons()
    {
        foreach (WeaponItem weapon in shop.weapons)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            weapon.SetUIForShop(ItemInMenu);
            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => shop.PurchaseItem(weapon, ItemTypes.Weapon, ItemInMenu));
            ItemInMenu.SetActive(true);
        }
    }

    void FillShopWithArmmors()
    {
        foreach (HelmetItem helmet in shop.helmets)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            helmet.SetUIForShop(ItemInMenu);
            ItemInMenu.SetActive(true);
        }

        foreach (ChestplateItem chestplate in shop.chestplates)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            chestplate.SetUIForShop(ItemInMenu);
            ItemInMenu.SetActive(true);
        }

        foreach (LeggingsItem leggings in shop.leggings)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            leggings.SetUIForShop(ItemInMenu);
            ItemInMenu.SetActive(true);
        }

        foreach (BootsItem boots in shop.boots)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            boots.SetUIForShop(ItemInMenu);
            ItemInMenu.SetActive(true);
        }
    }
}
