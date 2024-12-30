using AtticAdventures.Core;
using UnityEngine;
using UnityEngine.Events;

public class PriceCheck : MonoBehaviour
{
    private int score;

    public UnityEvent onValueLower;
    public UnityEvent onValueHigher;

    public void CheckPrice(int value)
    {
        score = GameManager.Instance.Score;

        if (value < score)
        {
            onValueLower?.Invoke();
            GameManager.Instance.AddScore(-value);
        }
        else
        {
            onValueHigher?.Invoke();
        }
    }
}
