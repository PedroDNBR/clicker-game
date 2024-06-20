using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInventory : MonoBehaviour
{
    public TMP_Text goldGameText;
    public TMP_Text goldShopText;

    public GameObject itemObject;
    public VerticalLayoutGroup itemGrid;

    public GameObject inventoryUIItem;

    public GameObject heroInventoryUIItem;

    public GameObject heroInventoryUIHelmet;
    public GameObject heroInventoryUIChestplate;
    public GameObject heroInventoryUILeggings;
    public GameObject heroInventoryUIBoots;

    public GameObject heroInventoryUIWeapon;

    public VerticalLayoutGroup heroItemGrid;

    public Button EquipHeroInField;


    public TMP_Text heroInventoryName;
    public TMP_Text heroInventoryStats;


    public Shop shop;

    public void Init()
    {
        Inventory.instance.onGoldChange += UpdateUI;
        shop.onItemPurchased += UpdateUI;
        UpdateUI();
    }

    void OnDisable()
    {
        Inventory.instance.onGoldChange -= UpdateUI;
        shop.onItemPurchased -= UpdateUI;
        UpdateUI();
    }

    private void Start()
    {
        CleanItemsFromGrid();
        FillInventoryWithHeroes();
    }

    void UpdateUI()
    {
        goldGameText.text = $"Gold: {Inventory.instance.GetGold()}";
        goldShopText.text = $"My gold: {Inventory.instance.GetGold()}";

        CleanItemsFromGrid();
        FillInventoryWithHeroes();
    }

    void CleanItemsFromGrid()
    {
        foreach (Transform child in itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void CleanHeroItemsFromGrid()
    {
        foreach (Transform child in heroItemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateHeroInUI(HeroItem hero)
    {
        GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
        ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = hero.sprite;

        ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{hero.name}\nShow";

        ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"{hero.maxBaseHealth} HP\n{hero.baseAttackPower} AT\n{hero.baseAttackSpeed} SP";

        ItemInMenu.GetComponent<Button>().onClick.AddListener(() => FillHeroInventory(hero));

        ItemInMenu.SetActive(true);
    }

    void SetHeroInField(HeroItem hero)
    {
        Inventory.instance.SetHeroInField(hero);
        EquipHeroInField.gameObject.SetActive(false);
    }

    public void FillHeroInventory(HeroItem hero)
    {
        if(hero.weapon) heroInventoryUIWeapon.GetComponent<Image>().sprite = hero.weapon.sprite;
        if(hero.helmet) heroInventoryUIHelmet.GetComponent<Image>().sprite = hero.helmet.sprite;
        if(hero.chestplate) heroInventoryUIChestplate.GetComponent<Image>().sprite = hero.chestplate.sprite;
        if(hero.leggings) heroInventoryUILeggings.GetComponent<Image>().sprite = hero.leggings.sprite;
        if(hero.boots) heroInventoryUIBoots.GetComponent<Image>().sprite = hero.boots.sprite;

        if (Inventory.instance.heroInField != null && Inventory.instance.heroInField == hero)
        {
            EquipHeroInField.gameObject.SetActive(false);
        }
        else
        {
            EquipHeroInField.gameObject.SetActive(true);
            EquipHeroInField.onClick.RemoveAllListeners();
            EquipHeroInField.onClick.AddListener(() => SetHeroInField(hero));
        }

        CleanHeroItemsFromGrid();
        FillHeroInventoryWithWeapons();
        FillHeroInventoryWithArmmors();

        ShowHeroInventory();
    }

    void FillHeroInventoryWithWeapons()
    {
        foreach (WeaponItem weapon in Inventory.instance.weapons)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = weapon.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{weapon.name}";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{weapon.attackPowerBoost} AT\n+{100 - (weapon.attackSpeedBoost * 100)} SP";
            ItemInMenu.SetActive(true);
        }
    }

    void FillHeroInventoryWithArmmors()
    {
        FillHeroInventoryWithHelmets();
        FillHeroInventoryChestplates();
        FillHeroInventoryWithLeggings();
        FillHeroInventoryWithBoots();
    }

    private void FillHeroInventoryWithBoots()
    {
        foreach (BootsItem boots in Inventory.instance.boots)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = boots.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{boots.name}";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{boots.armorPoints} A";
            ItemInMenu.SetActive(true);
        }
    }

    private void FillHeroInventoryWithLeggings()
    {
        foreach (LeggingsItem leggings in Inventory.instance.leggings)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = leggings.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{leggings.name}";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{leggings.armorPoints} A";
            ItemInMenu.SetActive(true);
        }
    }

    private void FillHeroInventoryChestplates()
    {
        foreach (ChestplateItem chestplate in Inventory.instance.chestplates)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = chestplate.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{chestplate.name}";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{chestplate.armorPoints} A";
            ItemInMenu.SetActive(true);
        }
    }

    private void FillHeroInventoryWithHelmets()
    {
        foreach (HelmetItem helmet in Inventory.instance.helmets)
        {
            GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = helmet.sprite;
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[0].text = $"{helmet.name}";
            ItemInMenu.GetComponentsInChildren<TMP_Text>()[1].text = $"+{helmet.armorPoints} A";
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
    }

    public void ShowInventoryWindow()
    {
        inventoryUIItem.SetActive(true);
    }

    public void HideInventoryWindow()
    {
        inventoryUIItem.SetActive(false);
    }
}
