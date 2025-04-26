using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SentenceManager : MonoBehaviour
{
    public InputField inputField;
    public GameObject[] sentencePrefab;
    public Transform sentenceArea;
    public float speakTime;
    public GameObject playerBulletPrefab;
    public Transform bulletArea; // 建議是 Canvas 下的空物件

    private List<Sentence> activeSentences = new List<Sentence>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShootBullet(inputField.text);
            inputField.text = "";
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnSentence", 1f, speakTime);
    }

 

    void CheckInput(string input)
    {
        foreach (Sentence sentence in activeSentences)
        {
            if (!sentence.IsTyping() && input == sentence.GetFullText())
            {
                sentence.DestroySentence();
                activeSentences.Remove(sentence);
                break;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("遊戲失敗：句子到底了！");
    }
    void ShootBullet(string text)
    {
        GameObject obj = Instantiate(playerBulletPrefab, bulletArea);
        obj.transform.localPosition = new Vector3(0f, -250f, 0f); // 玩家發射位置（可調整）
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.SetText(text);
    }
    public List<Sentence> GetActiveSentences()
    {
        return activeSentences;
    }
    public void RemoveSentence(Sentence sentence)
    {
        if (activeSentences.Contains(sentence))
        {
            activeSentences.Remove(sentence);
        }
    }

    public void SpawnSentence()
    {
        // 讓所有現有句子往下移
        foreach (Sentence s in activeSentences)
        {
            s.MoveDown(60f); // 每句往下 60 單位，可依照句子高度調整
        }

        // 新句子出現在最上面
        GameObject obj = Instantiate(sentencePrefab[Random.Range(0, sentencePrefab.Length)], sentenceArea);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f); // 顯示在最上方
        Sentence sentence = obj.GetComponent<Sentence>();
        sentence.SetSentence();
        activeSentences.Insert(0, sentence); // 加到最前面
    }

}
