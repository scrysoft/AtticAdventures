using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RealSoftGames
{
    public class ScrollBarFixedSize : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Scrollbar scrollBar;
        [SerializeField, Range(0f, 1f)] private float size = 0f;

        private void Start()
        {
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            scrollBar.size = size;
            scrollBar.value = 0.999f;
        }

        private void OnValueChanged(Vector2 arg0)
        {
            scrollBar.size = size;
        }
    }
}