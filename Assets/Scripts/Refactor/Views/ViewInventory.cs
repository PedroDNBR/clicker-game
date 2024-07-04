using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class ViewInventory : MonoBehaviour
    {
        [Header("Text")]
        public TextMeshProUGUI goldGameText;
        public TextMeshProUGUI goldShopText;
        public TextMeshProUGUI heroInventoryName;
        public TextMeshProUGUI heroInventoryStats;

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
            goldGameText.text = $"Gold: {Inventory.instance.Gold}";
            goldShopText.text = $"My gold: {Inventory.instance.Gold}";

            OpenHeroesInventory();
        }

        void UpdateHeroInventoryItemUI(string itemId, bool isEquipped, ItemTypes type, GameObject itemFrame)
        {
            BaseItem item = ItemDatabase.instance.GetItemById(itemId) as BaseItem;
            UpdateHeroStats();
            item.SetUIForInventory(itemFrame, isEquipped);
            switch (type)
            {
                case ItemTypes.Weapon:
                    heroInventoryUIWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SmallSpritePath);
                    break;
                case ItemTypes.Helmet:
                    heroInventoryUIHelmet.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SmallSpritePath);
                    break;
                case ItemTypes.Chestplate:
                    heroInventoryUIChestplate.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SmallSpritePath);
                    break;
                case ItemTypes.Leggings:
                    heroInventoryUILeggings.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SmallSpritePath);
                    break;
                case ItemTypes.Boots:
                    heroInventoryUIBoots.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SmallSpritePath);
                    break;
            }

            FilterItemListByType(Inventory.instance.SelectedItemType);
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

            FilterItemListAndSetType(Inventory.instance.SelectedItemType);
        }

        void FilterItemListAndSetType(ItemTypes type)
        {
            Inventory.instance.SelectedItemType = type;
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

        void InstantiateHeroInUI(string localId, string globalId)
        {
            if (itemGrid == null) return;
            HeroItem hero = ItemDatabase.instance.GetItemById(globalId) as HeroItem;
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
            ItemInMenu.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(hero.SpritePath);

            ItemInMenu.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{hero.ItemName}\nShow";

            ItemInMenu.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{hero.MaxBaseHealth} HP\n{hero.BaseAttackPower} AT\n{hero.BaseAttackSpeed} SP";

            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => FillHeroInventory(hero, localId));

            ItemInMenu.SetActive(true);
        }

        void SetHeroInField(HeroItem hero, string localId)
        {
            Inventory.instance.SetHeroInField(hero, localId);
            equipHeroInField.gameObject.SetActive(false);
        }

        void UpdateHeroStats()
        {
            HeroItem hero = Inventory.instance.SelectedHero;
            heroInventoryName.text = $"{hero.ItemName}";
            heroInventoryStats.text = $"HP:{hero.MaxBaseHealth} A:{hero.GetTotalArmor()} AT:{hero.GetTotalDamage()} SP:{hero.GetTotalAttackSpeed()}";
        }

        public void FillHeroInventory(HeroItem hero, string localId)
        {
            Inventory.instance.SelectedHero = hero;
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
                equipHeroInField.onClick.AddListener(() => SetHeroInField(hero, localId));
            }

            CleanHeroItemsFromGrid();
            FillHeroInventoryWithWeapons();
            FillHeroInventoryWithArmmors();

            ShowHeroInventory();

            SetHeroInventoryListeners();
        }

        private void SetHeroInventoryEquipmentSprites(HeroItem hero)
        {
            heroInventoryUIWeapon.GetComponent<Image>().sprite = !string.IsNullOrEmpty(hero.WeaponId) ? Resources.Load<Sprite>(Inventory.instance.GetWeaponItemByLocalId(hero.WeaponId).SpritePath) : heroInventoryUIWeaponDefaultIcon;

            heroInventoryUIHelmet.GetComponent<Image>().sprite = !string.IsNullOrEmpty(hero.HelmetId) ? Resources.Load<Sprite>(Inventory.instance.GetHelmetItemByLocalId(hero.HelmetId).SpritePath) : heroInventoryUIHelmetDefaultIcon;

            heroInventoryUIChestplate.GetComponent<Image>().sprite = !string.IsNullOrEmpty(hero.ChestplateId) ? Resources.Load<Sprite>(Inventory.instance.GetChestplateItemByLocalId(hero.ChestplateId).SpritePath) : heroInventoryUIChestplateDefaultIcon;

            heroInventoryUILeggings.GetComponent<Image>().sprite = !string.IsNullOrEmpty(hero.LeggingsId) ? Resources.Load<Sprite>(Inventory.instance.GetLeggingsItemByLocalId(hero.LeggingsId).SpritePath) : heroInventoryUILeggingsDefaultIcon;

            heroInventoryUIBoots.GetComponent<Image>().sprite = !string.IsNullOrEmpty(hero.BootsId) ? Resources.Load<Sprite>(Inventory.instance.GetBootsItemByLocalId(hero.BootsId).SpritePath) : heroInventoryUIBootsDefaultIcon;
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
            FillHeroInventoryWithItems(Inventory.instance.WeaponsId, ItemTypes.Weapon);
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
            FillHeroInventoryWithItems(Inventory.instance.HelmetsId, ItemTypes.Helmet);
        }

        void FillHeroInventoryWithChestplates()
        {
            FillHeroInventoryWithItems(Inventory.instance.ChestplatesId, ItemTypes.Chestplate);
        }

        void FillHeroInventoryWithLeggings()
        {
            FillHeroInventoryWithItems(Inventory.instance.LeggingsId, ItemTypes.Leggings);
        }

        void FillHeroInventoryWithBoots()
        {
            FillHeroInventoryWithItems(Inventory.instance.BootsId, ItemTypes.Boots);
        }

        // Dictionary<LocalId, InventoryItem(GlobalId, isEquipped)>
        void FillHeroInventoryWithItems(Dictionary<string, InventoryItem> itemsIdDictionary, ItemTypes itemType)
        {
            foreach (KeyValuePair<string, InventoryItem> item in itemsIdDictionary)
            {
                BaseItem itemObj = ItemDatabase.instance.GetItemById(item.Value.globalId) as BaseItem;
                if (itemType != itemObj.itemType) continue;
                GameObject ItemInMenu = Instantiate(itemObject, heroItemGrid.transform);
                itemObj.SetUIForInventory(ItemInMenu, item.Value.isEquipped);
                ItemInMenu.GetComponent<Button>().onClick.AddListener(() => Inventory.instance.EquipInHero(item.Key, item.Value.globalId, itemObj.itemType, ItemInMenu));
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
            Inventory.instance.SelectedItemType = ItemTypes.None;
        }

        void FillInventoryWithHeroes()
        {
            foreach (KeyValuePair<string, InventoryItem> item in Inventory.instance.HeroesId)
            {
                InstantiateHeroInUI(item.Key, item.Value.globalId);
            }
        }

        public void ToggleInventoryWindow()
        {
            inventoryUIItem.SetActive(!inventoryUIItem.activeSelf);
            if (inventoryUIItem.activeSelf) OpenHeroesInventory();
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
}
