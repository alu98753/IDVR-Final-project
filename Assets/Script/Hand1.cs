using Oculus.Interaction.PoseDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand1 : MonoBehaviour
{
    public OVRHand hand;
    public FingerFeatureStateProvider fingerFeatureStateProvider;
    public float rayDistance = 10f;
    public GameObject aimer;
    // public LineRenderer lineRenderer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsGOOD())
        {
            aimer.SetActive(true);

            aimming();

            print("isstop");

        }
        else
        {
            aimer.SetActive(false);
        }

    }


    void aimming()
    {
        Vector3 origin3 = aimer.transform.position;

        // 計算食指方向向量
        Vector3 direction = aimer.transform.forward;

        // 將方向向量normalize
        direction.Normalize();
        print("AIMMING");

        RaycastHit hit;
        if (Physics.Raycast(origin3, direction, out hit, rayDistance))//有打到東西
        {
            Debug.DrawRay(origin3, direction);
            GameObject author = hit.collider.gameObject;
            author.GetComponent<Author>().stopTalking();

            //lineRenderer.enabled = true; lineRenderer.SetPosition(0, origin3); lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            //lineRenderer.enabled = false;
            print("AIMMING NOTHIHG");
        }
        print("AIMMING2");
    }

    bool IsGOOD()
    {
        if (!GetFingerIsCurling(OVRHand.HandFinger.Thumb) &&
        GetFingerIsCurling(OVRHand.HandFinger.Index) &&
        GetFingerIsCurling(OVRHand.HandFinger.Middle) &&
        GetFingerIsCurling(OVRHand.HandFinger.Ring) &&
        GetFingerIsCurling(OVRHand.HandFinger.Pinky)
        )
        {
            Debug.LogError("stop talking");
            return true;
        }
        return false;
    }

    bool GetFingerIsCurling(OVRHand.HandFinger finger)
    {
        string s;
        var indexFingerState = fingerFeatureStateProvider.GetCurrentState((Oculus.Interaction.Input.HandFinger)finger, FingerFeature.Curl, out s);

        //print(s);
        return (s == "2" ? true : false);
    }


}
