using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "NPC/BaseItem")]
public class BaseItem : ScriptableObject
{
    [Header("Item info")]

    public string itemName;

    public Sprite sprite;

    public int price = 2;

    public void SetUIItemSprite(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<Image>()[1].sprite = sprite;
    }

    public void SetUIItemNameAndPrice(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{name}\n{price} Gold";
    }

    public void SetUIItemNameAndOwned(GameObject itemUI)
    {
        itemUI.GetComponentsInChildren<TMP_Text>()[0].text = $"{name}\nOwned";
    }

    public void DisableButton(GameObject itemUI)
    {
        itemUI.GetComponent<Button>().interactable = false;
    }

    public void SetUIItemAsOwned(GameObject itemUI)
    {
        SetUIItemNameAndOwned(itemUI);
        DisableButton(itemUI);
    }

    public virtual void SetUIForShop(GameObject itemUI)
    {
        SetUIItemSprite(itemUI);
        SetUIItemNameAndPrice(itemUI);
    }

}
