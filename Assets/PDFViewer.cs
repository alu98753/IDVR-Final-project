using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PDFViewer : MonoBehaviour
{
    public List<Texture2D> pages; // �s��Ҧ��������Ϥ�
    private int currentPageIndex = 0; // ������

    public Renderer planeRenderer;

    void Start()
    {
        if (pages.Count > 0)
        {
            // ��l�ƲĤ@��
            planeRenderer.material.mainTexture = pages[currentPageIndex];
        }
    }

    void Update()
    {
        // ���U A ���½��W�@��
        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousPage();
        }
        // ���U D ���½��U�@��
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

