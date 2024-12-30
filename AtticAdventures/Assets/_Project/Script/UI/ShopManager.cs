using AtticAdventures;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemCategory
{
    Stats,
    Weapon,
    Skill,
    Upgrade
}

[System.Serializable]
public class ShopItem
{
    public Sprite icon;
    public string title;
    public string description;
    public float factor;
    private float price;
    public bool isBuyable;
    public ItemCategory category;
}

public class ShopManager : MonoBehaviour
{
    [SerializeField] int maxBeadsCount;
    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField] Transform itemsParent;

    public List<ShopItem> shopItems;

    [ContextMenu("Initialize Shop")]
    public void InitializeShopContextMenu()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        foreach (var item in shopItems)
        {
            var panelInstance = Instantiate(itemPanelPrefab, itemsParent);
            panelInstance.name = $"{item.title}_{item.isBuyable}";
            panelInstance.GetComponent<ShopItemUI>().SetupItem(
                item.icon,
                item.title,
                item.description,
                maxBeadsCount * item.factor,
                item.isBuyable,
                item.category
            );
        }
    }

    [ContextMenu("Clear Shop")]
    public void ClearShop()
    {
        for (int i = itemsParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(itemsParent.GetChild(i).gameObject);
        }
    }

    public void ShowCategory(string categoryName)
    {
        if (categoryName == "All")
        {
            for (int i = 0; i < itemsParent.childCount; i++)
            {
                itemsParent.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (Enum.TryParse(categoryName, out ItemCategory category))
        {
            for (int i = 0; i < itemsParent.childCount; i++)
            {
                var child = itemsParent.GetChild(i);
                var itemUI = child.GetComponent<ShopItemUI>();
                if (itemUI != null)
                {
                    child.gameObject.SetActive(itemUI.category == category);
                }
            }
        }
    }
}
