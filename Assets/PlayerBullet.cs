using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class PlayerBullet : MonoBehaviour
{
    public float speed = 300f;
    public Text text;
    private string content;

    public void SetText(string input)
    {
        content = input;
        text.text = input;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 超出畫面就銷毀
        if (transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }

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

}
