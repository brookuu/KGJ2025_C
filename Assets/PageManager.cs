using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageManager : MonoBehaviour
{
    public Text pageText;       // 顯示頁面的 Text
    public Button prevButton;   // 上一頁按鈕
    public Button nextButton;   // 下一頁按鈕

    private int currentPage = 0;    // 當前頁面
    [TextArea]
    public string[] pages = new string[10]   // 預設10頁內容
    {
        "這是第1頁的內容",
        "這是第2頁的內容",
        "這是第3頁的內容",
        "這是第4頁的內容",
        "這是第5頁的內容",
        "這是第6頁的內容",
        "這是第7頁的內容",
        "這是第8頁的內容",
        "這是第9頁的內容",
        "這是第10頁的內容"
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
