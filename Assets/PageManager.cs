using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageManager : MonoBehaviour
{
    public Text pageText;       // ��ܭ����� Text
    public Button prevButton;   // �W�@�����s
    public Button nextButton;   // �U�@�����s

    private int currentPage = 0;    // ��e����
    [TextArea]
    public string[] pages = new string[10]   // �w�]10�����e
    {
        "�o�O��1�������e",
        "�o�O��2�������e",
        "�o�O��3�������e",
        "�o�O��4�������e",
        "�o�O��5�������e",
        "�o�O��6�������e",
        "�o�O��7�������e",
        "�o�O��8�������e",
        "�o�O��9�������e",
        "�o�O��10�������e"
    };

    void Start()
    {
        prevButton.onClick.AddListener(PrevPage);
        nextButton.onClick.AddListener(NextPage);
        UpdatePage();
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        pageText.text = pages[currentPage];
    }
}
