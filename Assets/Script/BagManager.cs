using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject PapersPrefab;
    private GameObject Paper;
    GameObject cameraRig;
    public int PaperInBag = 0;//沒有

    private void Awake()
    {
        PaperInBag = 0;
        cameraRig = GameObject.FindWithTag("MainCamera");
    }

    public void coutt()
    {
        if (PaperInBag == 2)
        {
            //Debug.LogError("hi");
            Vector3 offset = cameraRig.transform.forward * 0.3f;

            Paper = Instantiate(PapersPrefab, transform.position, Quaternion.identity);
            Paper.transform.position = cameraRig.transform.position + offset;
            PaperInBag = 1;//出現中
        }
    }
}
