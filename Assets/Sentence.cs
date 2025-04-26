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
    public List<string> destroyKeywords;  // 可消除的關鍵字
    public void SetSentence()
    {
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

        //transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

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
