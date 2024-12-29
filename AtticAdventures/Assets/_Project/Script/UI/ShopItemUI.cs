using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtticAdventures
{
    public class ShopItemUI : MonoBehaviour
    {
        public Sprite icon;
        public string title;
        public string description;
        public float price;
        public bool isBuyable;
        public ItemCategory category;

        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI titelText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] Button button;

        public void SetupItem(Sprite icon, string title, string description, float price, bool isBuyable, ItemCategory category)
        {
            this.icon = icon;
            this.title = title;
            this.description = description;
            this.price = price;
            this.isBuyable = isBuyable;
            this.category = category;

            UpdateItemUI();
        }

        private void UpdateItemUI() 
        {
            image.sprite = icon;
            titelText.text = title;
            descriptionText.text = description;
            priceText.text = price.ToString();
            button.interactable = isBuyable;
        }
    }
}
