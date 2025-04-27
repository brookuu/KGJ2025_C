using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuperBullet : MonoBehaviour
{
    public float speed = 250f;

    private float initialY;
    private float screenWidthWorld;

    public float maxScale = 6f;
    public float minScale = 1f;

    private float scaleDistanceThreshold = Screen.height; // 用來調整縮放比例範圍
    public float hitDistance = 30f; // 距離小於這個，就算打中

    void Start()
    {
        initialY = transform.position.y; // 記錄初始位置

    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        AdjustScaleBasedOnPosition();

        // 超出畫面就銷毀
        if (transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }

        CheckHitSentences();
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

    private void CheckHitSentences()
    {
        foreach (Sentence sentence in FindObjectOfType<SentenceManager>().GetActiveSentences().ToList())
        {
            if (!sentence.IsTyping() &&
                Vector2.Distance(transform.position, sentence.transform.position) < hitDistance)
            {
                // 打中
                FindObjectOfType<SentenceManager>().RemoveSentence(sentence);
                sentence.DestroySentence();
                break;
            }
        }
    }
}
