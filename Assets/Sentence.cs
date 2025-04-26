using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class Sentence : MonoBehaviour
{
    public Text text;

    private bool isTyping = false;
    private bool isActive = true;

    [TextArea] public string fullText; // ��ܤ��e
    public List<string> destroyKeywords; // �i����������r

    // �s�W���Ѽ�
    public float maxFallSpeed = 20f;    // �̤j�U���t��
    public float minFallSpeed = 10f;     // �̤p�U���t��
    public float maxScale = 2.5f;       // �̤j�ؤo
    public float minScale = 0.1f;       // �̤p�ؤo
    public float appearDuration = 1.0f; // ��j�ɶ��]��^

    private float appearTimer = 0f;
    private Vector3 startScale;
    private Vector3 targetScale = Vector3.one;

    public float initialY = 0f;  // �ΨӦs�x��ͦ��ɪ�Y��m�]�Z���ù����ߡ^

    public void SetSentence()
    {
        startScale = Vector3.one * 0.2f; // �@�}�l����p
        transform.localScale = startScale;
        appearTimer = 0f;
        initialY = transform.position.y; // �O���ͦ��ɪ� Y ��m
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        text.text = "";
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f); // ���r���j
        }
        isTyping = false;
    }

    void Update()
    {
        if (!isActive) return;

        // �ھ�Y��m�վ��j�ĪG
        if (appearTimer < appearDuration)
        {
            appearTimer += Time.deltaTime;
            float t = Mathf.Clamp01(appearTimer / appearDuration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        }

        // �p��P��lY��m���Z��
        float distanceFromInitial = Mathf.Abs(transform.position.y - initialY);

        // �ھڶZ���վ�U���t��
        float fallSpeed = Mathf.Lerp(maxFallSpeed, minFallSpeed, distanceFromInitial / Screen.height);

        // �ھڶZ���վ�y�l���j�p
        float scale = Mathf.Lerp(minScale, maxScale, distanceFromInitial / Screen.height);
        transform.localScale = new Vector3(scale, scale, 1);

        // �p��ó]�m�U���t��
        transform.localPosition += Vector3.down * fallSpeed * Time.deltaTime;

        // �ˬd�O�_�W�L�e��
        if (transform.position.y < -Screen.height)
        {
            isActive = false;
            FindObjectOfType<SentenceManager>().GameOver();
        }
    }

    public bool IsTyping() => isTyping;

    public void DestroySentence()
    {
        Destroy(gameObject);
    }

    public void MoveDown(float distance)
    {
        transform.localPosition -= new Vector3(0f, distance, 0f);
    }

    public string GetFullText()
    {
        return fullText;
    }

    public bool MatchesKeyword(string input)
    {
        return destroyKeywords.Any(k => input.Contains(k));
    }
}
