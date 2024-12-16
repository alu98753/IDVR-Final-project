using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperHover : MonoBehaviour
{
    //private Vector3 previousPosition;
    //private float swipeThreshold = 0.1f; // 設置敏感度
    public HandGrabInteractable handGrabInteractable;
    //private GameObject leftHand;
    private GameObject rightHand;
    public GameObject inner;
    private GameObject PPM;
    public List<Material> innerMaterial;
    public int it = 0;
    int nowDir = -1, lastDir = -1;
    private float cnt;
    private float TimeTherld = 1f;
    //private float accumulatedSwipeDistance_Horizontal = 0; // 累積滑動距離
    //private float accumulatedSwipeDistance_Vertical = 0; // 累積滑動距離

    public GameObject PapersBallPrefab;
    /*private LineRenderer forwardRay;
    private LineRenderer upRay;
    private LineRenderer rightRay;*/

    void Awake()
    {
        // 獲取手部追蹤物件
        //leftHand = GameObject.FindWithTag("LeftHandAnchor");
        rightHand = GameObject.FindWithTag("RightFingerAnchor");

        PPM = GameObject.Find("PaperPageManager");

        int hold = it;
        while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
        {
            it = (it + 1) % innerMaterial.Count; // 如果在跳過列表中，跳過該頁
            if (it == hold)
            {
                it = -1;
                break;//當前那頁也要跳=全沒
            }
        }
        if (it == -1)
        {
            inner.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
            inner.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            inner.GetComponent<Renderer>().material = innerMaterial[it];
        }

        //forwardRay = CreateLineRenderer(Color.red);
        //upRay = CreateLineRenderer(Color.black);
        //rightRay = CreateLineRenderer(Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateLineRenderer(forwardRay, rightHand.transform.position, rightHand.transform.position + rightHand.transform.forward * 2);
        //UpdateLineRenderer(upRay, rightHand.transform.position, rightHand.transform.position + rightHand.transform.up * 2);
        //UpdateLineRenderer(rightRay, rightHand.transform.position, rightHand.transform.position + rightHand.transform.right * 2);
        //Debug.LogError(rightHand.transform.rotation * rightHand.transform.position + " " + rightHand.transform.rotation * Vector3.forward + " " + rightHand.transform.rotation * Vector3.up);

        if (handGrabInteractable.State == InteractableState.Hover)
        {
            cnt += Time.deltaTime;
            //DetectSwipeGesture(rightHand);
        }
        else
        {
            cnt = 0;
            //accumulatedSwipeDistance_Horizontal = 0;
            //accumulatedSwipeDistance_Vertical = 0;
        }
        HandleDir();
        //Debug.LogError(nowDir + " " + lastDir);
        if (cnt >= TimeTherld)
        {//(accumulatedSwipeDistance_Horizontal > swipeThreshold || accumulatedSwipeDistance_Vertical > swipeThreshold )&& 
            HandleSwipe();
            //accumulatedSwipeDistance_Horizontal = 0;
            //accumulatedSwipeDistance_Vertical = 0;
            cnt = 0;
        }
        //Debug.LogError(cnt);
    }

    /*private void DetectSwipeGesture(GameObject hand)
    {
        // 取得手部的當前位置
        Vector3 currentPosition = hand.transform.position;

        // 計算與上一次位置的變化
        Vector3 swipeDelta = currentPosition - previousPosition;

        // 投影到紅色軸（向右水平）和綠色軸（向下縱向）
        float deltaHorizontal = Vector3.Dot(swipeDelta, hand.transform.right);
        float deltaVertical = Vector3.Dot(swipeDelta, hand.transform.up);
        //Debug.LogError(deltaHorizontal + " " + deltaVertical);
        // 滑動距離累積
        //accumulatedSwipeDistance += Mathf.Max(Mathf.Abs(deltaHorizontal), Mathf.Abs(deltaVertical));

        //Debug.LogError(cnt);

        // 判斷手部的移動方向來識別滑動手勢
        if (Mathf.Abs(deltaHorizontal) > Mathf.Abs(deltaVertical))
        {
            if (deltaHorizontal <= 0)// 左滑
            {
                dir = 1;
            }
            else// 右滑
            {
                dir = 0;
            }
            accumulatedSwipeDistance_Horizontal += Mathf.Abs(deltaHorizontal);
        }
        else
        {
            if (deltaVertical > 0)
            {
                // 上滑
                dir = 2;
            }
            else
            {
                // 下滑
                dir = 3;
            }
            accumulatedSwipeDistance_Vertical += Mathf.Abs(deltaVertical);
        }

        // 更新前一次位置
        previousPosition = currentPosition;
    }*/

    private void HandleDir()//右左上下
    {
        float hold1, hold2, hold3;
        hold1 = Vector3.Distance(rightHand.transform.position, transform.Find("UpArea").position);
        hold2 = Vector3.Distance(rightHand.transform.position, transform.Find("RightArea").position);
        hold3 = Vector3.Distance(rightHand.transform.position, transform.Find("LeftArea").position);
        if (hold1 < hold2 && hold1 < hold3)
        {
            if(nowDir != 2) lastDir = nowDir;
            nowDir = 2;
            return;
        }
        else if (hold2 < hold1 && hold2 < hold3)
        {
            if (nowDir != 0) lastDir = nowDir;
            nowDir = 0;
            return;
        }
        else
        {
            if (nowDir != 1) lastDir = nowDir;
            nowDir = 1;
            return;
        }
    }

    private void HandleSwipe()
    {
        if (it == -1) return;
        if (nowDir == 1 && lastDir == 0)
        {
            int hold = it;
            // 左滑
            it = (it + 1) % innerMaterial.Count;
            // 檢查頁數是否在跳過列表中
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it + 1) % innerMaterial.Count; // 如果在跳過列表中，跳過該頁
                if (it == hold) break;//只剩當前那頁
            }
            inner.GetComponent<Renderer>().material = innerMaterial[it];
            Debug.LogError(it);
        }
        else if (nowDir == 0 && lastDir == 1)
        {
            int hold = it;
            // 右滑
            it = (it - 1 + innerMaterial.Count) % innerMaterial.Count;
            // 檢查頁數是否在跳過列表中
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it - 1 + innerMaterial.Count) % innerMaterial.Count; // 如果在跳過列表中，跳過該頁
                if (it == hold) break;//只剩當前那頁
            }
            inner.GetComponent<Renderer>().material = innerMaterial[it];
            Debug.LogError(it);
        }
        else if(nowDir == 2)
        {
            int hold = it;
            // 上滑
            PPM.GetComponent<PaperPageManager>().skippedPages.Add(it);
            it = (it + 1) % innerMaterial.Count;
            // 檢查頁數是否在跳過列表中
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it + 1) % innerMaterial.Count; // 如果在跳過列表中，跳過該頁
                if (it == hold)
                {//只剩當前那頁
                    it = -1;
                    break;
                }
            }
            if (it == -1)
            {
                inner.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
                inner.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                inner.GetComponent<Renderer>().material = innerMaterial[it];
            }
            // 生成紙球並設置飛行
            GeneratePaperBall(hold);
            Debug.LogError("bye: " + hold + ", hi: " + it);
        }
    }

    private void GeneratePaperBall(int page)
    {
        // 在paper上面位置生成紙球
        Vector3 spawnPosition = transform.Find("PaperUp").position;
        GameObject paperBall = Instantiate(PapersBallPrefab, spawnPosition, Quaternion.identity);

        // 設置紙球文字
        var textComponent = paperBall.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = page.ToString();
        }

        // 添加力讓紙球飛出去
        Rigidbody rb = paperBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forceDirection = transform.forward + transform.up;//rightHand.transform.forward + rightHand.transform.up
            rb.AddForce(forceDirection.normalized * 2f, ForceMode.Impulse); // 設定初速度
        }
    }

    /*private LineRenderer CreateLineRenderer(Color color)
    {
        // 創建一個空物件作為 LineRenderer 的宿主
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 設置 LineRenderer 的基本屬性
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        return lineRenderer;
    }

    private void UpdateLineRenderer(LineRenderer lineRenderer, Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }*/
}
