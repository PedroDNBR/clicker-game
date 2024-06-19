using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewInventory : MonoBehaviour
{
    public TMP_Text goldGameText;
    public TMP_Text goldShopText;

    public Inventory inventory;
    public Shop shop;

    public void Init()
    {
        inventory = GetComponent<Inventory>();
        inventory.onGoldChange += UpdateUI;
        shop.onItemPurchased += UpdateUI;
        UpdateUI();
    }

    void OnDisable()
    {
        inventory.onGoldChange -= UpdateUI;
        shop.onItemPurchased -= UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        goldGameText.text = $"Gold: {inventory.GetGold()}";
        goldShopText.text = $"My gold: {inventory.GetGold()}";
    }
}
