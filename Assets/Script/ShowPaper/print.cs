using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class print : MonoBehaviour
{
    public GameObject PapersPrefab;
    private GameObject Paper;
    GameObject cameraRig;

    private void Awake()
    {
        cameraRig = GameObject.FindWithTag("MainCamera");
    }

    public void coutt()
    {
        //Debug.LogError("hi");
        Vector3 offset = cameraRig.transform.forward * 0.3f;

        Paper = Instantiate(PapersPrefab, transform.position, Quaternion.identity);
        Paper.transform.position = cameraRig.transform.position + offset;
        //Destroy(this.gameObject);
    }
}
