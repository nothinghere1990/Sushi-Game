using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public TMP_Text countDownTimerText;
    public float remainingTime;

    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countDownTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}