using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVolumeDebugger : MonoBehaviour
{
    private AudioClip micClip;
    private bool isMicInitialized = false;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            micClip = Microphone.Start(null, true, 1, 44100);
            isMicInitialized = true;
            Debug.Log("麥克風開始錄音！");
        }
        else
        {
            Debug.LogError("找不到麥克風！");
        }
    }

    void Update()
    {
        if (!isMicInitialized) return;

        Debug.Log("目前麥克風錄音時間位置：" + Microphone.GetPosition(null));

        float volume = GetMicVolume();
        Debug.Log("目前麥克風音量：" + volume.ToString("F4"));
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
        return Mathf.Sqrt(sum / sampleSize);
    }
}
