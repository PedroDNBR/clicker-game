using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewShop : MonoBehaviour
{
    public GameObject ItemObject;
    public VerticalLayoutGroup ItemGrid;

    public GameObject ShopItem;

    public Inventory inventory;
    public Shop shop;

    private void Start()
    {
        CleanItemsFromGrid();
        FillShopWithHeroes();
    }

    public void OpenShopWindow()
    {
        ShopItem.SetActive(!ShopItem.activeSelf);
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
        foreach (Transform child in ItemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void FillShopWithHeroes()
    {
        foreach (HeroItem hero in shop.heroes)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = hero.sprite;
            if(inventory.heroes.Find(heroInArray => heroInArray == hero))
            {
                ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{hero.name}\nPurchased";
                ItemInMenu.GetComponent<Button>().interactable = false;
            }
            else
            {
                ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{hero.name}\n{hero.price} Gold";
            }

            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"{hero.maxBaseHealth} HP\n{hero.baseAttackPower} AT\n{hero.baseAttackSpeed} SP";

            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => shop.PurchaseHero(hero, ItemInMenu));

            ItemInMenu.SetActive(true);
        }
    }

    void FillShopWithWeapons()
    {
        foreach (WeaponItem weapon in shop.weapons)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = weapon.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{weapon.name}\n{weapon.price} Gold";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{weapon.attackPowerBoost} AT\n+{100 - (weapon.attackSpeedBoost * 100)} SP";
            ItemInMenu.SetActive(true);
        }
    }

    void FillShopWithArmmors()
    {
        foreach (HelmetItem helmet in shop.helmets)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = helmet.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{helmet.name}\n{helmet.price} Gold";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{helmet.armorPoints} A";
            ItemInMenu.SetActive(true);
        }

        foreach (ChestplateItem chestplate in shop.chestplates)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = chestplate.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{chestplate.name}\n{chestplate.price} Gold";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{chestplate.armorPoints} A";
            ItemInMenu.SetActive(true);
        }

        foreach (LeggingsItem leggings in shop.leggings)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = leggings.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{leggings.name}\n{leggings.price} Gold";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{leggings.armorPoints} A";
            ItemInMenu.SetActive(true);
        }

        foreach (BootsItem boots in shop.boots)
        {
            GameObject ItemInMenu = Instantiate(ItemObject, ItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = boots.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{boots.name}\n{boots.price} Gold";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{boots.armorPoints} A";
            ItemInMenu.SetActive(true);
        }
    }
}
