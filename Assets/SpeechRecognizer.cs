using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeechRecognizer : MonoBehaviour
{
    public float threshold = 0.3f; // 聲音大小門檻，大於這個就觸發
    private AudioClip micClip;
    private bool isMicInitialized = false;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            micClip = Microphone.Start(null, true, 1, 44100);
            isMicInitialized = true;
        }
        else
        {
            Debug.LogError("沒有偵測到麥克風！");
        }
    }

    void Update()
    {
        if (!isMicInitialized) return;

        float volume = GetMicVolume();
        if (volume > threshold)
        {
            Debug.Log("大叫偵測到！！觸發技能！");
            // 在這裡放你要觸發的技能邏輯
        }
    }

    float GetMicVolume()
    {
        int sampleSize = 128;
        float[] samples = new float[sampleSize];
        int micPosition = Microphone.GetPosition(null) - sampleSize + 1;
        if (micPosition < 0) return 0f;

        micClip.GetData(samples, micPosition);
        float sum = 0f;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += samples[i] * samples[i];
        }
        return Mathf.Sqrt(sum / sampleSize); // 平均能量的平方根
    }
}
