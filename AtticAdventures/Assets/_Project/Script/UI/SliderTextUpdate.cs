using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextUpdate : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private string[] textValues;

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateText);
        UpdateText(slider.value);
    }

    private void UpdateText(float value)
    {
        int index = Mathf.Clamp(Mathf.RoundToInt(value), 0, textValues.Length - 1);
        textDisplay.text = textValues[index];
    }
}
