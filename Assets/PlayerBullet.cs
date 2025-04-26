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

    // �ΨӰO�����a�l�u����l Y ��m
    private float initialY;

    // �̤j�P�̤p���Y����
    public float maxScale =3f;
    public float minScale = 0.5f;

    // �]�w��l��m���d��
    private float scaleDistanceThreshold = Screen.height; // �Ψӽվ��Y���ҽd��

    public void SetText(string input)
    {
        content = input;
        text.text = input;
    }

    void Start()
    {
        initialY = transform.position.y; // �O����l��m
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // �ھڤl�u�� Y ��m�վ��Y��
        AdjustScaleBasedOnPosition();

        // �W�X�e���N�P��
        if (transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }

        // �ˬd�l�u�P�y�l�������I��
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

    // �ھ� Y ��m�վ�l�u���Y��
    private void AdjustScaleBasedOnPosition()
    {
        // �p��l�u�� Y ��m�M��l Y ��m���Z��
        float distanceFromInitial = Mathf.Abs(transform.position.y - initialY);

        // �ھڤl�u�� Y �Z���ӭp���Y����
        float scale = Mathf.Lerp(maxScale, minScale, distanceFromInitial / scaleDistanceThreshold);

        // �]�w�l�u���Y��
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
