///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 22:47
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        private void OnEnable()
        {
            Player.OnCurrencyUpdate += OnCurrencyChange;
        }

        private void OnDisable()
        {
            Player.OnCurrencyUpdate -= OnCurrencyChange;
        }

        private void Awake()
        {
            if (text == null)
                text = GetComponent<TMP_Text>();
        }

        public void OnCurrencyChange(int value)
        {
            text.text = "Silver: " + value;
        }
    }
}