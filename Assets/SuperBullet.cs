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

    private float scaleDistanceThreshold = Screen.height; // �Ψӽվ��Y���ҽd��
    public float hitDistance = 30f; // �Z���p��o�ӡA�N�⥴��

    void Start()
    {
        initialY = transform.position.y; // �O����l��m

    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        AdjustScaleBasedOnPosition();

        // �W�X�e���N�P��
        if (transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }

        CheckHitSentences();
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

    private void CheckHitSentences()
    {
        foreach (Sentence sentence in FindObjectOfType<SentenceManager>().GetActiveSentences().ToList())
        {
            if (!sentence.IsTyping() &&
                Vector2.Distance(transform.position, sentence.transform.position) < hitDistance)
            {
                // ����
                FindObjectOfType<SentenceManager>().RemoveSentence(sentence);
                sentence.DestroySentence();
                break;
            }
        }
    }
}
