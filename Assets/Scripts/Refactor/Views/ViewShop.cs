using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class ViewShop : MonoBehaviour
    {
        public GameObject itemObject;
        public VerticalLayoutGroup itemGrid;

        public GameObject shopItem;

        private void Start()
        {
            CleanItemsFromGrid();
            FillShopWithHeroes();
        }

        public void ToggleShopWindow()
        {
            shopItem.SetActive(!shopItem.activeSelf);
            if (shopItem.activeSelf) OpenHeroesShop();
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

        void InstantiateHeroInUI(string id, HeroItem hero)
        {
            GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);

            hero.SetUIItemSprite(ItemInMenu);

            if (hero.Owned)
            {
                hero.SetUIItemNameAndOwned(ItemInMenu);
                hero.DisableButton(ItemInMenu);
            }
            else
            {
                hero.SetUIItemNameAndPrice(ItemInMenu);
            }

            hero.SetUIIBaseItemStats(ItemInMenu);

            ItemInMenu.GetComponent<Button>().onClick.AddListener(() => Shop.instance.PurchaseItem(id, hero, hero.ItemType, ItemInMenu));

            ItemInMenu.SetActive(true);
        }

        void FillShopWithHeroes()
        {
            foreach (KeyValuePair<string, IBaseItem> item in ItemDatabase.instance.Items)
            {
                if(item.Value is HeroItem)
                {
                    HeroItem hero = item.Value as HeroItem;
                    if (Level.instance.GetLevel() >= hero.minLevelToUnlock)
                    {
                        InstantiateHeroInUI(item.Key, hero);
                    }
                }
                
            }
        }

        void FillShopWithWeapons()
        {
            FillShopWithItems<WeaponItem>();
        }

        void FillShopWithArmmors()
        {
            FillShopWithItems<ArmorItem>();
        }

        void FillShopWithItems<IBaseItem>()
        {
            foreach (KeyValuePair<string, RC.IBaseItem> item in ItemDatabase.instance.Items)
            {
                if (item.Value is IBaseItem)
                {
                    BaseItem itemObj = item.Value as BaseItem;
                    if (Level.instance.GetLevel() >= itemObj.minLevelToUnlock)
                    {
                        GameObject ItemInMenu = Instantiate(itemObject, itemGrid.transform);
                        itemObj.SetUIForShop(ItemInMenu);
                        ItemInMenu.GetComponent<Button>().onClick.AddListener(() => Shop.instance.PurchaseItem(item.Key, item.Value, itemObj.ItemType, ItemInMenu));
                        ItemInMenu.SetActive(true);
                    }
                }
            }
        }
    }
}
