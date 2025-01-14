using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private UnityEvent onUnderOneMinute;

    private float currentTime;
    private bool isRunning;

    private bool isBossDead = false;

    private void Update()
    {
        if (isRunning && !isBossDead)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 3599f)
            {
                currentTime = 3599f;
                isRunning = false;
            }
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        if (isBossDead) return;

        currentTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        StopTimerAndCheckTime();
    }

    public void StopTimerAndCheckTime()
    {
        isRunning = false;
        if (currentTime < 60f)
        {
            onUnderOneMinute?.Invoke();
        }
    }

    public void SetBossIsDead()
    {
        isBossDead = true;
    }
}
