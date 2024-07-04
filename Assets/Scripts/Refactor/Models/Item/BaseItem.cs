using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    [CreateAssetMenu(menuName = "GameAssets/NPC/BaseItem")]
    public class BaseItem : ScriptableObject, IBaseItem
    {
        [Header("Item info")]
        [JsonIgnore] public string itemName;
        [JsonIgnore] public string spritePath;
        [JsonIgnore] public string smallSpritePath;
        [JsonIgnore] public int price;
        [JsonIgnore] public int minLevelToUnlock;
        [JsonIgnore] public ItemTypes itemType;

        public string ItemName { get => itemName; set => itemName = value; }
        public string SpritePath { get => spritePath; set => spritePath = value; }
        public string SmallSpritePath { get => smallSpritePath; set => smallSpritePath = value; }
        public int Price { get => price; set => price = value; }
        public int ShoppedItemCount { get; set; }
        public int MinLevelToUnlock { get => minLevelToUnlock; set => minLevelToUnlock = value; }
        public ItemTypes ItemType { get => itemType; set => itemType = value; }


        public void SetUIItemSprite(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(spritePath);
        }

        public void SetUIItemNameAndPrice(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{ItemName}\n{Price} Gold";
        }

        public void SetUIItemNameAndOwned(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{ItemName}\nOwned";
        }

        public void SetUIItemNameAndEquipped(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{ItemName}\nEquipped";
        }

        public void SetUIItemName(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{ItemName}\n ";
        }

        public void DisableButton(GameObject itemUI)
        {
            itemUI.GetComponent<Button>().interactable = false;
        }

        public void EnableButton(GameObject itemUI)
        {
            itemUI.GetComponent<Button>().interactable = true;
        }

        public void SetUIItemAsOwned(GameObject itemUI)
        {
            SetUIItemNameAndOwned(itemUI);
            DisableButton(itemUI);
        }

        public void SetUIItemAsEquipped(GameObject itemUI)
        {
            SetUIItemNameAndEquipped(itemUI);
            DisableButton(itemUI);
        }

        public virtual void SetUIForShop(GameObject itemUI)
        {
            SetUIItemSprite(itemUI);
            SetUIItemNameAndPrice(itemUI);
            EnableButton(itemUI);
        }

        public virtual void SetUIForInventory(GameObject itemUI, bool equipped)
        {
            SetUIItemSprite(itemUI);
            if (equipped)
                SetUIItemAsEquipped(itemUI);
            else
                SetUIItemName(itemUI);

            SetUIIItemStats(itemUI);

        }

        public virtual void SetUIIItemStats(GameObject itemUI)
        {
            itemUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"";
        }

        public void SetUIForInventory(GameObject itemUI)
        {
            throw new System.NotImplementedException();
        }
    }
}