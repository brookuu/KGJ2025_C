using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SentenceManager : MonoBehaviour
{
    public InputField inputField;
    public GameObject[] sentencePrefab;
    public Transform sentenceArea;
    public float spacing = 20f; // 每個句子之間固定的小間距（比如20）

    public GameObject playerBulletPrefab;
    public Transform bulletArea;
    public float speakTime;

    private List<Sentence> activeSentences = new List<Sentence>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnSentence), 1f, speakTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShootBullet(inputField.text);
            inputField.text = "";
        }
    }

    void ShootBullet(string text)
    {
        GameObject obj = Instantiate(playerBulletPrefab, bulletArea);
        obj.transform.localPosition = new Vector3(0f, -150f, 0f);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.SetText(text);
    }

    public void GameOver()
    {
        Debug.Log("遊戲失敗：句子到底了！");
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
        // 生成新句子
        GameObject obj = Instantiate(sentencePrefab[Random.Range(0, sentencePrefab.Length)], sentenceArea);
        Sentence sentence = obj.GetComponent<Sentence>();
        sentence.SetSentence();
        activeSentences.Insert(0, sentence);

        // 排列位置
        float currentY = 0f;
        for (int i = 0; i < activeSentences.Count; i++)
        {
            RectTransform rt = activeSentences[i].GetComponent<RectTransform>();

            // 先強制更新Layout，避免剛生成preferredHeight拿到0
            LayoutRebuilder.ForceRebuildLayoutImmediate(rt);

            float height = rt.sizeDelta.y;
            activeSentences[i].transform.localPosition = new Vector3(0f, currentY, 0f);

            currentY -= height + spacing; // 下一個往下
        }
    }

}
