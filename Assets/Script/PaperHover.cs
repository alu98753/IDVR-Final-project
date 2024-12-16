using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperHover : MonoBehaviour
{
    //private Vector3 previousPosition;
    //private float swipeThreshold = 0.1f; // �]�m�ӷP��
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
    //private float accumulatedSwipeDistance_Horizontal = 0; // �ֿn�ưʶZ��
    //private float accumulatedSwipeDistance_Vertical = 0; // �ֿn�ưʶZ��

    public GameObject PapersBallPrefab;
    /*private LineRenderer forwardRay;
    private LineRenderer upRay;
    private LineRenderer rightRay;*/

    void Awake()
    {
        // ����ⳡ�l�ܪ���
        //leftHand = GameObject.FindWithTag("LeftHandAnchor");
        rightHand = GameObject.FindWithTag("RightFingerAnchor");

        PPM = GameObject.Find("PaperPageManager");

        int hold = it;
        while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
        {
            it = (it + 1) % innerMaterial.Count; // �p�G�b���L�C���A���L�ӭ�
            if (it == hold)
            {
                it = -1;
                break;//��e�����]�n��=���S
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
        // ���o�ⳡ����e��m
        Vector3 currentPosition = hand.transform.position;

        // �p��P�W�@����m���ܤ�
        Vector3 swipeDelta = currentPosition - previousPosition;

        // ��v�����b�]�V�k�����^�M���b�]�V�U�a�V�^
        float deltaHorizontal = Vector3.Dot(swipeDelta, hand.transform.right);
        float deltaVertical = Vector3.Dot(swipeDelta, hand.transform.up);
        //Debug.LogError(deltaHorizontal + " " + deltaVertical);
        // �ưʶZ���ֿn
        //accumulatedSwipeDistance += Mathf.Max(Mathf.Abs(deltaHorizontal), Mathf.Abs(deltaVertical));

        //Debug.LogError(cnt);

        // �P�_�ⳡ�����ʤ�V���ѧO�ưʤ��
        if (Mathf.Abs(deltaHorizontal) > Mathf.Abs(deltaVertical))
        {
            if (deltaHorizontal <= 0)// ����
            {
                dir = 1;
            }
            else// �k��
            {
                dir = 0;
            }
            accumulatedSwipeDistance_Horizontal += Mathf.Abs(deltaHorizontal);
        }
        else
        {
            if (deltaVertical > 0)
            {
                // �W��
                dir = 2;
            }
            else
            {
                // �U��
                dir = 3;
            }
            accumulatedSwipeDistance_Vertical += Mathf.Abs(deltaVertical);
        }

        // ��s�e�@����m
        previousPosition = currentPosition;
    }*/

    private void HandleDir()//�k���W�U
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
            // ����
            it = (it + 1) % innerMaterial.Count;
            // �ˬd���ƬO�_�b���L�C��
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it + 1) % innerMaterial.Count; // �p�G�b���L�C���A���L�ӭ�
                if (it == hold) break;//�u�ѷ�e����
            }
            inner.GetComponent<Renderer>().material = innerMaterial[it];
            Debug.LogError(it);
        }
        else if (nowDir == 0 && lastDir == 1)
        {
            int hold = it;
            // �k��
            it = (it - 1 + innerMaterial.Count) % innerMaterial.Count;
            // �ˬd���ƬO�_�b���L�C��
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it - 1 + innerMaterial.Count) % innerMaterial.Count; // �p�G�b���L�C���A���L�ӭ�
                if (it == hold) break;//�u�ѷ�e����
            }
            inner.GetComponent<Renderer>().material = innerMaterial[it];
            Debug.LogError(it);
        }
        else if(nowDir == 2)
        {
            int hold = it;
            // �W��
            PPM.GetComponent<PaperPageManager>().skippedPages.Add(it);
            it = (it + 1) % innerMaterial.Count;
            // �ˬd���ƬO�_�b���L�C��
            while (PPM.GetComponent<PaperPageManager>().skippedPages.Contains(it))
            {
                it = (it + 1) % innerMaterial.Count; // �p�G�b���L�C���A���L�ӭ�
                if (it == hold)
                {//�u�ѷ�e����
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
            // �ͦ��Ȳy�ó]�m����
            GeneratePaperBall(hold);
            Debug.LogError("bye: " + hold + ", hi: " + it);
        }
    }

    private void GeneratePaperBall(int page)
    {
        // �bpaper�W����m�ͦ��Ȳy
        Vector3 spawnPosition = transform.Find("PaperUp").position;
        GameObject paperBall = Instantiate(PapersBallPrefab, spawnPosition, Quaternion.identity);

        // �]�m�Ȳy��r
        var textComponent = paperBall.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = page.ToString();
        }

        // �K�[�O���Ȳy���X�h
        Rigidbody rb = paperBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forceDirection = transform.forward + transform.up;//rightHand.transform.forward + rightHand.transform.up
            rb.AddForce(forceDirection.normalized * 2f, ForceMode.Impulse); // �]�w��t��
        }
    }

    /*private LineRenderer CreateLineRenderer(Color color)
    {
        // �Ыؤ@�ӪŪ���@�� LineRenderer ���J�D
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // �]�m LineRenderer �����ݩ�
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
