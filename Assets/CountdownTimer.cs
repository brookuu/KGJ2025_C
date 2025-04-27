using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour
{
    public Text timerText;       // 用來顯示倒數時間的 Text
    public int minutes = 1;      // 初始分鐘
    public int seconds = 30;     // 初始秒數

    private float timeRemaining; // 剩餘時間（秒）
    private bool isRunning = false;

    void Start()
    {
        timeRemaining = minutes * 60 + seconds;
        isRunning = true;
        UpdateTimerText(); // 先更新一次畫面
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            TimerEnd();
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int displayMinutes = Mathf.FloorToInt(timeRemaining / 60);
        int displaySeconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);
    }

    void TimerEnd()
    {
        Debug.Log("倒數結束！");
        // 這裡可以加你倒數結束後要做的事情
    }

    public void StartTimer(int min, int sec)
    {
        minutes = min;
        seconds = sec;
        timeRemaining = minutes * 60 + seconds;
        isRunning = true;
        UpdateTimerText();
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
