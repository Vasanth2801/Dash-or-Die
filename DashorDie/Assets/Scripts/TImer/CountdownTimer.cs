using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] float timer = 30f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timeToDecrease = 1f;
    [SerializeField] Slider timeSlider;

    public delegate void HandleGameEnd();
    public static event HandleGameEnd OnGameEnd;

    void Start()
    {
        timeSlider.maxValue = timer;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        float remainingTime = timer;
        while(timer > 0)
        {
            timeSlider.value = timer;
            timerText.text = "Timer: " + Mathf.Ceil(timer).ToString();
            yield return new WaitForSeconds(timeToDecrease);
            timer--;
        }

        timeSlider.value = 0;
        timerText.text = "Timer: " + Mathf.Ceil(timer).ToString();
        OnGameEnd?.Invoke();
    }
}
