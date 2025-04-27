using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour
{
    public Text timerText;       // �Ψ���ܭ˼Ʈɶ��� Text
    public int minutes = 1;      // ��l����
    public int seconds = 30;     // ��l���

    private float timeRemaining; // �Ѿl�ɶ��]��^
    private bool isRunning = false;

    void Start()
    {
        timeRemaining = minutes * 60 + seconds;
        isRunning = true;
        UpdateTimerText(); // ����s�@���e��
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
        Debug.Log("�˼Ƶ����I");
        // �o�̥i�H�[�A�˼Ƶ�����n�����Ʊ�
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
