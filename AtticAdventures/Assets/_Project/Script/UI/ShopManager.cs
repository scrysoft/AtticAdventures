using AtticAdventures;
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

            panelInstance.GetComponent<ShopItemUI>().SetupItem(item.icon, item.title, item.description, maxBeadsCount * item.factor, item.isBuyable, item.category);
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
}
