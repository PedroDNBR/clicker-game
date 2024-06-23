using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInventory : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text goldGameText;
    public TMP_Text goldShopText;
    public TMP_Text heroInventoryName;
    public TMP_Text heroInventoryStats;

    [Header("UI Template")]
    public GameObject itemObject;
    public VerticalLayoutGroup itemGrid;
    public GameObject inventoryUIItem;
    public GameObject heroInventoryUIItem;
    public VerticalLayoutGroup heroItemGrid;
    public Button equipHeroInField;

    [Header("UI Equipment Template")]
    public Button heroInventoryUIHelmet;
    public Sprite heroInventoryUIHelmetDefaultIcon;

    public Button heroInventoryUIChestplate;
    public Sprite heroInventoryUIChestplateDefaultIcon;

    public Button heroInventoryUILeggings;
    public Sprite heroInventoryUILeggingsDefaultIcon;

    public Button heroInventoryUIBoots;
    public Sprite heroInventoryUIBootsDefaultIcon;


    public Button heroInventoryUIWeapon;
    public Sprite heroInventoryUIWeaponDefaultIcon;

    public Button heroInventoryUIUnequip;

    public void Init()
    {
        Inventory.instance.onGoldChange += UpdateUI;
        Inventory.instance.onHeroItemEquipped += UpdateHeroInventoryItemUI;
        Inventory.instance.onHeroItemUnequip += UpdateHeroInventoryItemUIDefault;
        Shop.instance.onItemPurchased += UpdateUI;
        UpdateUI();
    }

    void OnDisable()
    {
        Inventory.instance.onGoldChange -= UpdateUI;
        Shop.instance.onItemPurchased -= UpdateUI;
        UpdateUI();
    }

    private void Start()
    {
        OpenHeroesInventory();
    }

    void OpenHeroesInventory()
    {
        CleanItemsFromGrid();
        FillInventoryWithHeroes();
    }

    void UpdateUI()
    {
        goldGameText.text = $"Gold: {Inventory.instance.GetGold()}";
        goldShopText.text = $"My gold: {Inventory.instance.GetGold()}";

        OpenHeroesInventory();
    }

    void UpdateHeroInventoryItemUI(BaseItem item, ItemTypes type, GameObject itemFrame)
    {
        if (item == null) return;
        UpdateHeroStats();
        item.SetUIForInventory(itemFrame);
        switch (type)
        {
            case ItemTypes.Weapon:
                heroInventoryUIWeapon.GetComponent<Image>().sprite = item.smallSprite;
                break;
            case ItemTypes.Helmet:
                heroInventoryUIHelmet.GetComponent<Image>().sprite = item.smallSprite;
                break;
            case ItemTypes.Chestplate:
                heroInventoryUIChestplate.GetComponent<Image>().sprite = item.smallSprite;
                break;
            case ItemTypes.Leggings:
                heroInventoryUILeggings.GetComponent<Image>().sprite = item.smallSprite;
                break;
            case ItemTypes.Boots:
                heroInventoryUIBoots.GetComponent<Image>().sprite = item.smallSprite;
                break;
        }

        FilterItemListByType(Inventory.instance.GetSelectedItemType());
    }

    void UpdateHeroInventoryItemUIDefault(ItemTypes type)
    {
        UpdateHeroStats();
        switch (type)
        {
            case ItemTypes.Weapon:
                heroInventoryUIWeapon.GetComponent<Image>().sprite = heroInventoryUIWeaponDefaultIcon;
                break;
            case ItemTypes.Helmet:
                heroInventoryUIHelmet.GetComponent<Image>().sprite = heroInventoryUIHelmetDefaultIcon;
                break;
            case ItemTypes.Chestplate:
                heroInventoryUIChestplate.GetComponent<Image>().sprite = heroInventoryUIChestplateDefaultIcon;
                break;
            case ItemTypes.Leggings:
                heroInventoryUILeggings.GetComponent<Image>().sprite = heroInventoryUILeggingsDefaultIcon;
                break;
            case ItemTypes.Boots:
                heroInventoryUIBoots.GetComponent<Image>().sprite = heroInventoryUIBootsDefaultIcon;
                break;
        }

        FilterItemListAndSetType(Inventory.instance.GetSelectedItemType());
    }

    void FilterItemListAndSetType(ItemTypes type)
    {
        Inventory.instance.SetSelectedItemType(type);
        FilterItemListByType(type);
    }

    private void FilterItemListByType(ItemTypes type)
    {
        CleanHeroItemsFromGrid();
        switch (type)
        {
            case ItemTypes.Weapon:
                FillHeroInventoryWithWeapons();
                break;
            case ItemTypes.Helmet:
                FillHeroInventoryWithHelmets();
                break;
            case ItemTypes.Chestplate:
                FillHeroInventoryWithChestplates();
                break;
            case ItemTypes.Leggings:
                FillHeroInventoryWithLeggings();
                break;
            case ItemTypes.Boots:
                FillHeroInventoryWithBoots();
                break;
            case ItemTypes.None:
                FillHeroInventoryWithWeapons();
                FillHeroInventoryWithArmmors();
                break;
        }
    }

    void CleanItemsFromGrid()
    {
        if (itemGrid == null) return;
        foreach (Transform child in itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void CleanHeroItemsFromGrid()
    {
        if (heroItemGrid == null) return;
        foreach (Transform child in heroItemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateHeroInUI(HeroItem hero)
    {
        if (itemGrid == null) return;
        GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
        ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = hero.sprite;

        ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{hero.itemName}\nShow";

        ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"{hero.maxBaseHealth} HP\n{hero.baseAttackPower} AT\n{hero.baseAttackSpeed} SP";

        ItemInMenu.GetComponent<Button>().onClick.AddListener(() => FillHeroInventory(hero));

        ItemInMenu.SetActive(true);
    }

    void SetHeroInField(HeroItem hero)
    {
        Inventory.instance.SetHeroInField(hero);
        equipHeroInField.gameObject.SetActive(false);
    }

    void UpdateHeroStats()
    {
        HeroItem hero = Inventory.instance.GetSelectedHero();
        heroInventoryName.text = $"{hero.itemName}";
        heroInventoryStats.text = $"HP:{hero.maxBaseHealth} A:{hero.GetTotalArmor()} AT:{hero.GetTotalDamage()} SP:{hero.GetTotalAttackSpeed()}";
    }

    public void FillHeroInventory(HeroItem hero)
    {
        Inventory.instance.SetSelectedHero(hero);
        SetHeroInventoryEquipmentSprites(hero);

        UpdateHeroStats();

        if (Inventory.instance.GetHeroInField() != null && Inventory.instance.GetHeroInField() == hero)
        {
            equipHeroInField.gameObject.SetActive(false);
        }
        else
        {
            equipHeroInField.gameObject.SetActive(true);
            equipHeroInField.onClick.RemoveAllListeners();
            equipHeroInField.onClick.AddListener(() => SetHeroInField(hero));
        }

        CleanHeroItemsFromGrid();
        FillHeroInventoryWithWeapons();
        FillHeroInventoryWithArmmors();

        ShowHeroInventory();

        SetHeroInventoryListeners();
    }

    private void SetHeroInventoryEquipmentSprites(HeroItem hero)
    {
        heroInventoryUIWeapon.GetComponent<Image>().sprite = hero.weapon ? hero.weapon.sprite : heroInventoryUIWeaponDefaultIcon;
        heroInventoryUIHelmet.GetComponent<Image>().sprite = hero.helmet ? hero.helmet.sprite : heroInventoryUIHelmetDefaultIcon;
        heroInventoryUIChestplate.GetComponent<Image>().sprite = hero.chestplate ? hero.chestplate.sprite : heroInventoryUIChestplateDefaultIcon;
        heroInventoryUILeggings.GetComponent<Image>().sprite = hero.leggings ? hero.leggings.sprite : heroInventoryUILeggingsDefaultIcon;
        heroInventoryUIBoots.GetComponent<Image>().sprite = hero.boots ? hero.boots.sprite : heroInventoryUIBootsDefaultIcon;
    }

    private void SetHeroInventoryListeners()
    {
        heroInventoryUIHelmet.onClick.RemoveAllListeners();
        heroInventoryUIHelmet.onClick.AddListener(() => FilterItemListAndSetType(ItemTypes.Helmet));

        heroInventoryUIChestplate.onClick.RemoveAllListeners();
        heroInventoryUIChestplate.onClick.AddListener(() => FilterItemListAndSetType(ItemTypes.Chestplate));

        heroInventoryUILeggings.onClick.RemoveAllListeners();
        heroInventoryUILeggings.onClick.AddListener(() => FilterItemListAndSetType(ItemTypes.Leggings));

        heroInventoryUIBoots.onClick.RemoveAllListeners();
        heroInventoryUIBoots.onClick.AddListener(() => FilterItemListAndSetType(ItemTypes.Boots));

        heroInventoryUIWeapon.onClick.RemoveAllListeners();
        heroInventoryUIWeapon.onClick.AddListener(() => FilterItemListAndSetType(ItemTypes.Weapon));

        heroInventoryUIUnequip.onClick.RemoveAllListeners();
        heroInventoryUIUnequip.onClick.AddListener(Inventory.instance.UnequipSelectedItemType);
    }

    void FillHeroInventoryWithWeapons()
    {
        FillHeroInventoryWithItems(new List<BaseItem>(Inventory.instance.weapons), ItemTypes.Weapon);
    }

    void FillHeroInventoryWithArmmors()
    {
        FillHeroInventoryWithHelmets();
        FillHeroInventoryWithChestplates();
        FillHeroInventoryWithLeggings();
        FillHeroInventoryWithBoots();
    }

    void FillHeroInventoryWithHelmets()
    {
        FillHeroInventoryWithItems(new List<BaseItem>(Inventory.instance.helmets), ItemTypes.Helmet);
    }

    void FillHeroInventoryWithChestplates()
    {
        FillHeroInventoryWithItems(new List<BaseItem>(Inventory.instance.chestplates), ItemTypes.Chestplate);
    }

    void FillHeroInventoryWithLeggings()
    {
        FillHeroInventoryWithItems(new List<BaseItem>(Inventory.instance.leggings), ItemTypes.Leggings);
    }

    void FillHeroInventoryWithBoots()
    {
        FillHeroInventoryWithItems(new List<BaseItem>(Inventory.instance.boots), ItemTypes.Boots);
    }

    void FillHeroInventoryWithItems(List<BaseItem> itemList, ItemTypes ItemType)
    {
        foreach (BaseItem item in itemList)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            item.SetUIForInventory(ItemInMenu);
            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => Inventory.instance.EquipInHero(item, ItemType, ItemInMenu));
            ItemInMenu.SetActive(true);
        }
    }

    public void ShowHeroInventory()
    {
        heroInventoryUIItem.SetActive(true);
    }

    public void HideHeroInventory()
    {
        heroInventoryUIItem.SetActive(false);
        // Inventory.instance.SetHeroInField(null);
        Inventory.instance.SetSelectedItemType(ItemTypes.None);
    }

    void FillInventoryWithHeroes()
    {   
        foreach (HeroItem hero in Inventory.instance.heroes)
        {
            InstantiateHeroInUI(hero);
        }
    }

    public void TogglInventoryWindow()
    {
        inventoryUIItem.SetActive(!inventoryUIItem.activeSelf);
        if(inventoryUIItem.activeSelf) OpenHeroesInventory();
    }

    public void ShowInventoryWindow()
    {
        inventoryUIItem.SetActive(true);
        OpenHeroesInventory();
    }

    public void HideInventoryWindow()
    {
        inventoryUIItem.SetActive(false);
    }
}
