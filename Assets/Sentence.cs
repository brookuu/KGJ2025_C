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

    [TextArea] public string fullText; // 顯示內容
    public List<string> destroyKeywords; // 可消除的關鍵字

    // 新增的參數
    public float maxFallSpeed = 20f;    // 最大下落速度
    public float minFallSpeed = 10f;     // 最小下落速度
    public float maxScale = 2.5f;       // 最大尺寸
    public float minScale = 0.1f;       // 最小尺寸
    public float appearDuration = 1.0f; // 放大時間（秒）

    private float appearTimer = 0f;
    private Vector3 startScale;
    private Vector3 targetScale = Vector3.one;

    public float initialY = 0f;  // 用來存儲剛生成時的Y位置（距離螢幕中心）

    public void SetSentence()
    {
        startScale = Vector3.one * 0.2f; // 一開始比較小
        transform.localScale = startScale;
        appearTimer = 0f;
        initialY = transform.position.y; // 記錄生成時的 Y 位置
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        text.text = "";
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f); // 打字間隔
        }
        isTyping = false;
    }

    void Update()
    {
        if (!isActive) return;

        // 根據Y位置調整放大效果
        if (appearTimer < appearDuration)
        {
            appearTimer += Time.deltaTime;
            float t = Mathf.Clamp01(appearTimer / appearDuration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        }

        // 計算與初始Y位置的距離
        float distanceFromInitial = Mathf.Abs(transform.position.y - initialY);

        // 根據距離調整下落速度
        float fallSpeed = Mathf.Lerp(maxFallSpeed, minFallSpeed, distanceFromInitial / Screen.height);

        // 根據距離調整句子的大小
        float scale = Mathf.Lerp(minScale, maxScale, distanceFromInitial / Screen.height);
        transform.localScale = new Vector3(scale, scale, 1);

        // 計算並設置下落速度
        transform.localPosition += Vector3.down * fallSpeed * Time.deltaTime;

        // 檢查是否超過畫面
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
