using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeechRecognizer : MonoBehaviour
{
    public float threshold = 0.3f; // �n���j�p���e�A�j��o�ӴNĲ�o
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
            Debug.LogError("�S����������J���I");
        }
    }

    void Update()
    {
        if (!isMicInitialized) return;

        float volume = GetMicVolume();
        if (volume > threshold)
        {
            Debug.Log("�j�s������I�IĲ�o�ޯ�I");
            // �b�o�̩�A�nĲ�o���ޯ��޿�
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
        return Mathf.Sqrt(sum / sampleSize); // ������q�������
    }
}
