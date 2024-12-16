using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnchorTrigger : MonoBehaviour
{
    GameObject cameraRig; // CameraRig 的 Transform
    public float triggerDistance = 0.5f; // 設定觸發距離為 0.3 
    public GameObject buttonPrefab;
    private GameObject currentButton;
    bool show = false;

    private void Awake()
    {
        cameraRig = GameObject.FindWithTag("MainCamera");
        show = false;
    }
    private void Update()
    {
        // 計算 CameraRig 和 Anchor 之間的距離
        float distance = Vector3.Distance(cameraRig.transform.position, transform.position + transform.forward * -0.05f);
        // 計算從 Anchor 到 CameraRig 的方向
        Vector3 directionToCamera = (cameraRig.transform.position - transform.position).normalized;

        //Debug.LogError(distance);

        // 如果距離小於 0.5，觸發事件
        if (distance < triggerDistance && !show)
        {
            Vector3 buttonPosition = transform.position + directionToCamera * 0.05f;
            currentButton = Instantiate(buttonPrefab, buttonPosition, Quaternion.identity);
            show = true;
        }
        else if (distance >= triggerDistance && show)
        {
            Destroy(currentButton);
            show = false;
        }
    }
}
