///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public class CategoryBtn : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Variables

        private Image image;
        private UnityEngine.UI.Toggle toggle;
        [SerializeField] private GameObject achievementContent;
        [SerializeField] private GameObject categoriesContent;
        [SerializeField] private TMP_InputField searchField;
        [SerializeField] private SpriteTransition transition;
        [SerializeField] private Color normal;
        [SerializeField] private Color highlighted;
        [SerializeField] private Color pressed;
        [SerializeField] private List<CategoryList> categories;

        [SerializeField] private GameObject normalObj;
        [SerializeField] private GameObject selectedObj;
        [SerializeField] private GameObject highlightedObj;

        public Sprite NormalSpeite { get => normalObj.GetComponent<Image>().sprite; set => normalObj.GetComponent<Image>().sprite = value; }
        public Sprite SelectedSprite { get => selectedObj.GetComponent<Image>().sprite; set => selectedObj.GetComponent<Image>().sprite = value; }
        public Sprite HighlightedSprite { get => highlightedObj.GetComponent<Image>().sprite; set => highlightedObj.GetComponent<Image>().sprite = value; }

        public enum SpriteTransition
        {
            ColorTint,
            SpriteSwap,
            //Animation
        }

        private enum ImageState
        {
            Pressed,
            Highlighted,
            Normal
        }

        #endregion Variables

        #region Properties

        public GameObject AchievementContent { get => achievementContent; set => achievementContent = value; }
        public GameObject CategoriesContent { get => categoriesContent; set => categoriesContent = value; }
        public TMP_InputField SearchField { get => searchField; set => searchField = value; }
        public SpriteTransition Transition { get => transition; set => transition = value; }
        public Color Normal { get => normal; set => normal = value; }
        public Color Highlighted { get => Highlighted; set => highlighted = value; }
        public Color Pressed { get => pressed; set => pressed = value; }
        public List<CategoryList> Categories { get => categories; set => categories = value; }
        public UnityEngine.UI.Toggle Toggle
        {
            get
            {
                if (toggle == null)
                    toggle = GetComponent<UnityEngine.UI.Toggle>();
                return toggle;
            }

            set => toggle = value;
        }


        #endregion Properties

        protected virtual void Start()
        {
            if (achievementContent == null)
                achievementContent = GameObject.Find("AchievementSystemAchievementContent");

            if (categoriesContent == null)
                categoriesContent = GameObject.Find("AchievementSystemCategoriesContent");

            CheckImageNotNUll();
            if (searchField == null)
                searchField = GameObject.Find("AchievementSystemSearchField").GetComponent<TMP_InputField>();
        }

        private void CheckImageNotNUll()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
                if (image == null)
                    image = normalObj.GetComponent<Image>();
            }
        }

        public virtual void CategoriesBtn()
        {
            AchievementManager.Instance.SearchAchievements();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Toggle.isOn)
            {
                SetHighLighted();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Toggle.isOn)
            {
                SetNormal();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            for (int i = 0; i < categoriesContent.transform.childCount; i++)
            {
                if (categoriesContent.transform.GetChild(i).gameObject != this.gameObject)
                    categoriesContent.transform.GetChild(i).GetComponent<CategoryBtn>().SetNormal();
                else
                    SetPressed();
            }
        }

        public virtual void SetNormal()
        {
            CheckImageNotNUll();

            Toggle.isOn = false;
            switch (Transition)
            {
                case SpriteTransition.ColorTint:
                    image.color = normal;
                    break;

                case SpriteTransition.SpriteSwap:
                    SetImageState(ImageState.Normal);
                    break;
            }
            GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }

        public virtual void SetHighLighted()
        {
            CheckImageNotNUll();

            switch (Transition)
            {
                case SpriteTransition.ColorTint:
                    image.color = highlighted;
                    break;

                case SpriteTransition.SpriteSwap:
                    SetImageState(ImageState.Highlighted);
                    break;
            }
        }

        public virtual void SetPressed()
        {
            CheckImageNotNUll();

            Toggle.isOn = true;
            switch (Transition)
            {
                case SpriteTransition.ColorTint:
                    image.color = pressed;
                    break;

                case SpriteTransition.SpriteSwap:
                    SetImageState(ImageState.Pressed);
                    break;
            }
            CategoriesBtn();
        }

        public virtual void SetupPressedState()
        {
            CheckImageNotNUll();

            Toggle.isOn = true;

            switch (Transition)
            {
                case SpriteTransition.ColorTint:
                    image.color = pressed;
                    break;

                case SpriteTransition.SpriteSwap:
                    SetImageState(ImageState.Pressed);
                    break;
            }
        }

        private void SetImageState(ImageState state)
        {
            switch (state)
            {
                case ImageState.Normal:
                    normalObj.SetActive(true);
                    selectedObj.SetActive(false);
                    highlightedObj.SetActive(false);
                    break;

                case ImageState.Highlighted:
                    normalObj.SetActive(false);
                    selectedObj.SetActive(false);
                    highlightedObj.SetActive(true);
                    break;

                case ImageState.Pressed:
                    normalObj.SetActive(false);
                    selectedObj.SetActive(true);
                    highlightedObj.SetActive(false);
                    break;
            }
        }
    }
}