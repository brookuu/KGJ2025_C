using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class PlayerBullet : MonoBehaviour
{
    public float speed = 250f;
    public Text text;
    private string content;

    // 用來記錄玩家子彈的初始 Y 位置
    private float initialY;

    // 最大與最小的縮放比例
    public float maxScale =3f;
    public float minScale = 0.5f;

    // 設定初始位置的範圍
    private float scaleDistanceThreshold = Screen.height; // 用來調整縮放比例範圍

    public void SetText(string input)
    {
        content = input;
        text.text = input;
    }

    void Start()
    {
        initialY = transform.position.y; // 記錄初始位置
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 根據子彈的 Y 位置調整縮放
        AdjustScaleBasedOnPosition();

        // 超出畫面就銷毀
        if (transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }

        // 檢查子彈與句子之間的碰撞
        foreach (Sentence sentence in FindObjectOfType<SentenceManager>().GetActiveSentences().ToList())
        {
            if (!sentence.IsTyping() &&
                Vector2.Distance(transform.position, sentence.transform.position) < 30f &&
                sentence.MatchesKeyword(content))
            {
                FindObjectOfType<SentenceManager>().RemoveSentence(sentence);
                sentence.DestroySentence();
                Destroy(gameObject);
                break;
            }
        }
    }

    // 根據 Y 位置調整子彈的縮放
    private void AdjustScaleBasedOnPosition()
    {
        // 計算子彈的 Y 位置和初始 Y 位置的距離
        float distanceFromInitial = Mathf.Abs(transform.position.y - initialY);

        // 根據子彈的 Y 距離來計算縮放比例
        float scale = Mathf.Lerp(maxScale, minScale, distanceFromInitial / scaleDistanceThreshold);

        // 設定子彈的縮放
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
