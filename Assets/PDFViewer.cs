using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PDFViewer : MonoBehaviour
{
    public List<Texture2D> pages; // 存放所有頁面的圖片
    private int currentPageIndex = 0; // 頁索引

    public Renderer planeRenderer;

    void Start()
    {
        if (pages.Count > 0)
        {
            // 初始化第一頁
            planeRenderer.material.mainTexture = pages[currentPageIndex];
        }
    }

    void Update()
    {
        // 按下 A 鍵時翻到上一頁
        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousPage();
        }
        // 按下 D 鍵時翻到下一頁
        if (Input.GetKeyDown(KeyCode.D))
        {
            NextPage();
        }
    }

    public void NextPage()
    {
        if (pages.Count == 0) return;

        currentPageIndex = (currentPageIndex + 1) % pages.Count; 
        planeRenderer.material.mainTexture = pages[currentPageIndex];
    }

    public void PreviousPage()
    {
        if (pages.Count == 0) return;

        currentPageIndex = (currentPageIndex - 1 + pages.Count) % pages.Count; 
        planeRenderer.material.mainTexture = pages[currentPageIndex];
    }
}

