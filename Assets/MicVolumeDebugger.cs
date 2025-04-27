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
            Debug.Log("���J���}�l�����I");
        }
        else
        {
            Debug.LogError("�䤣����J���I");
        }
    }

    void Update()
    {
        if (!isMicInitialized) return;

        Debug.Log("�ثe���J�������ɶ���m�G" + Microphone.GetPosition(null));

        float volume = GetMicVolume();
        Debug.Log("�ثe���J�����q�G" + volume.ToString("F4"));
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
