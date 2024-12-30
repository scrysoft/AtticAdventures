using AtticAdventures.Core;
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
        public float score;

        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titelText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button button;
        [SerializeField] private Image iconBackground;
        [SerializeField] private Image panelBackground;
        [SerializeField] private Color disabledColor;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color disabledTextColor;
        [SerializeField] private Color normalTextColor;
        [SerializeField] private Color notAffordableColor;
        [SerializeField] private GameObject stamp;

        private float transparency = 0.3f;

        public void SetupItem(
            Sprite icon,
            string title,
            string description,
            float price,
            bool isBuyable,
            ItemCategory category
        )
        {
            this.icon = icon;
            this.title = title;
            this.description = description;
            this.price = price;
            this.isBuyable = isBuyable;
            this.category = category;
            UpdateItemUI();
            UpdateBackgrounds(isBuyable);
        }

        private void UpdateItemUI()
        {
            image.sprite = icon;
            titelText.text = title;
            descriptionText.text = description;
            priceText.text = price.ToString();
            button.interactable = isBuyable;
        }

        private void UpdateBackgrounds(bool isBuyable)
        {
            if (isBuyable)
            {
                iconBackground.color = normalColor;
                panelBackground.color = normalColor;
                image.color = normalTextColor;
                titelText.color = normalTextColor;
                descriptionText.color = normalTextColor;

                stamp.SetActive(false);
            }
            else
            {
                iconBackground.color = new Color(disabledColor.r, disabledColor.g, disabledColor.b, transparency);
                panelBackground.color = new Color(disabledColor.r, disabledColor.g, disabledColor.b, transparency);
                image.color = new Color(disabledTextColor.r, disabledTextColor.g, disabledTextColor.b, transparency);
                titelText.color = new Color(disabledTextColor.r, disabledTextColor.g, disabledTextColor.b, transparency);
                descriptionText.color = new Color(disabledTextColor.r, disabledTextColor.g, disabledTextColor.b, transparency);
                priceText.color = new Color(disabledTextColor.r, disabledTextColor.g, disabledTextColor.b, transparency);

                stamp.SetActive(true);

                RectTransform stampRect = stamp.GetComponent<RectTransform>();
                stampRect.localEulerAngles = new Vector3(0f, 0f, Random.Range(-15f, 15f));

                priceText.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (isBuyable && priceText.gameObject.activeSelf)
            {
                priceText.color = (GameManager.Instance.Score > price) ? normalTextColor : notAffordableColor;
            }
        }
    }
}
