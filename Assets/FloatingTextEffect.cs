using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingTextEffect : MonoBehaviour
{
    public Text text;
    public float duration = 4f;
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 1f);
    public Vector3 endScale = new Vector3(1.5f, 1.5f, 1f);
    public Vector2 startPosition = new Vector2(0f, -300f);
    public Vector2 endPosition = new Vector2(0f, 200f);

    void Start()
    {
        StartCoroutine(PlayEffect());
    }

    IEnumerator PlayEffect()
    {
        RectTransform rect = GetComponent<RectTransform>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

        rect.anchoredPosition = startPosition;
        transform.localScale = startScale;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // 插值移動 + 放大 + 淡出
            rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            float alpha = Mathf.Lerp(1f, 0f, t);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
