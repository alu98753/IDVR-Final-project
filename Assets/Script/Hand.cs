using Oculus.Interaction.PoseDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hand : MonoBehaviour
{
    public OVRHand hand;
    public FingerFeatureStateProvider fingerFeatureStateProvider;
    public float rayDistance = 10f;
    public GameObject aimer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop())
        {
            aimer.SetActive(true);

            aimming();






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

        print("AAAAAAAAAAA" + origin3);
        RaycastHit hit;
        if (Physics.Raycast(origin3, direction, out hit, rayDistance))//有打到東西
        {
            GameObject author = hit.collider.gameObject;
            author.GetComponent<Author>().stopTalking();
        }

    }

    bool IsStop()
    {
        if (!GetFingerIsCurling(OVRHand.HandFinger.Thumb) &&
        !GetFingerIsCurling(OVRHand.HandFinger.Index) &&
        !GetFingerIsCurling(OVRHand.HandFinger.Middle) &&
        !GetFingerIsCurling(OVRHand.HandFinger.Ring) &&
        !GetFingerIsCurling(OVRHand.HandFinger.Pinky)
        )
        {
            print("stop talking");
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
