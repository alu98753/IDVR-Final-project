using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConnector : MonoBehaviour
{
    public int id;
    public GameObject objectA; // 起始物件
    public GameObject objectB; // 終點物件
    public LineRenderer lineRenderer; // 用於顯示曲線
    public int curveResolution = 20; // 曲線點的數量，用於控制平滑度
    public float curveHeight = 2f; // 曲線的高度，用於調整弧度

    public bool StartPoint;
    void Start()
    {
        if (StartPoint)
        {
            objectA = this.gameObject;
        }
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (objectB == null && StartPoint)
        {
            FindTheTarget();
        }
        if (objectA != null && objectB != null && lineRenderer != null && StartPoint)
        {
            DrawCurve();
        }
    }

    void DrawCurve()
    {
        // 確定起始點和方向
        Vector3 startPoint = objectA.transform.position;
        Vector3 startDirection = objectA.transform.forward;

        // 確定終點和方向
        Vector3 endPoint = objectB.transform.position;
        Vector3 endDirection = objectB.transform.forward;

        // 計算控制點，用於生成曲線
        Vector3 controlPoint1 = startPoint + startDirection * curveHeight;
        Vector3 controlPoint2 = endPoint + endDirection * curveHeight;

        // 計算貝塞爾曲線的點
        List<Vector3> curvePoints = new List<Vector3>();
        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i / (float)curveResolution;
            Vector3 curvePoint = CalculateBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
            curvePoints.Add(curvePoint);
        }

        // 設置 LineRenderer 的點
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // 使用四次貝塞爾曲線公式
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // 起點的權重
        point += 3 * uu * t * p1; // 第一控制點的權重
        point += 3 * u * tt * p2; // 第二控制點的權重
        point += ttt * p3; // 終點的權重

        return point;
    }

    public void FindTheTarget()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("connector");
        foreach (GameObject i in list)
        {
            if (i.GetComponent<ObjectConnector>().id == id && i != objectA)
            {
                objectB = i;
                break;
            }
        }
    }

}
