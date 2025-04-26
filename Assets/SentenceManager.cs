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
    public Transform bulletArea; // ��ĳ�O Canvas �U���Ū���

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
        Debug.Log("�C�����ѡG�y�l�쩳�F�I");
    }
    void ShootBullet(string text)
    {
        GameObject obj = Instantiate(playerBulletPrefab, bulletArea);
        obj.transform.localPosition = new Vector3(0f, -250f, 0f); // ���a�o�g��m�]�i�վ�^
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
        // ���Ҧ��{���y�l���U��
        foreach (Sentence s in activeSentences)
        {
            s.MoveDown(60f); // �C�y���U 60 ���A�i�̷ӥy�l���׽վ�
        }

        // �s�y�l�X�{�b�̤W��
        GameObject obj = Instantiate(sentencePrefab[Random.Range(0, sentencePrefab.Length)], sentenceArea);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f); // ��ܦb�̤W��
        Sentence sentence = obj.GetComponent<Sentence>();
        sentence.SetSentence();
        activeSentences.Insert(0, sentence); // �[��̫e��
    }

}
