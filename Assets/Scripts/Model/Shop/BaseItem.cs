using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "NPC/BaseItem")]
public class BaseItem : ScriptableObject
{
    [Header("Item info")]

    public string itemName;

    public Sprite sprite;

    public Sprite smallSprite;

    public int price = 2;

    private bool equipped = false;

    public int shoppedItemCount = -1;

    public int minLevelToUnlock = 0;

    public void SetUIItemSprite(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<Image>()[1].sprite = sprite;
    }

    public void SetUIItemNameAndPrice(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{itemName}\n{price} Gold";
    }

    public void SetUIItemNameAndOwned(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{itemName}\nOwned";
    }

    public void SetUIItemNameAndEquipped(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{itemName}\nEquipped";
    }

    public void SetUIItemName(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{itemName}\n ";
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

    public virtual void SetUIForInventory(GameObject itemUI)
    {
        SetUIItemSprite(itemUI);
        if (equipped)
            SetUIItemAsEquipped(itemUI);
        else
            SetUIItemName(itemUI);

        SetUIIItemStats(itemUI);

    }

    public bool GetEquipped()
    {
        return equipped;
    }

    public void SetEquipped(bool equipped)
    {
        this.equipped = equipped;
    }

    public virtual void SetUIIItemStats(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[1].text = $"";
    }

}
