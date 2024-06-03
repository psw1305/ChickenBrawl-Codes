using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text alertTimerText;

    private void Start()
    {
        alertTimerText.text = "";
        alertTimerText.transform.ToScaleLoop();
    }

    public void DisplayTime(float timeToDisplay)
    {
        SetDisplayTimer(timeToDisplay, timerText);
    }

    public void DisplayTime()
    {
        timerText.text = "00:00";
    }

    public void DisplayAlertTime(float timeToDisplay)
    {
        SetDisplayTimer(timeToDisplay, alertTimerText);
    }

    public void DisplayAlertTime()
    {
        alertTimerText.text = "00:00";
    }

    private void SetDisplayTimer(float timeToDisplay, TMP_Text textTimer)
    {
        timeToDisplay += 1;

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
