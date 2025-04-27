using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuperBullet : MonoBehaviour
{
    public float speed = 250f;

    private float initialX;
    private float screenWidthWorld;

    public float maxScale = 6f;
    public float minScale = 1f;
    private float scaleDistanceThreshold;

    public float hitDistance = 50f; // 距離小於這個，就算打中

    void Start()
    {
        initialX = transform.position.x;

        // 螢幕右邊界轉成世界座標
        Vector3 screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        screenWidthWorld = screenRight.x + 1f;

        scaleDistanceThreshold = screenWidthWorld * 0.5f;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        AdjustScaleBasedOnPosition();

        if (transform.position.x > screenWidthWorld)
        {
            Destroy(gameObject);
        }

        CheckHitSentences();
    }

    private void AdjustScaleBasedOnPosition()
    {
        float distanceFromInitial = Mathf.Abs(transform.position.x - initialX);
        float scale = Mathf.Lerp(maxScale, minScale, distanceFromInitial / scaleDistanceThreshold);
        transform.localScale = new Vector3(scale, scale, 1f);
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
                Destroy(gameObject);
                break;
            }
        }
    }
}
