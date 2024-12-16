using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnchorTrigger : MonoBehaviour
{
    GameObject cameraRig; // CameraRig �� Transform
    public float triggerDistance = 0.5f; // �]�wĲ�o�Z���� 0.3 
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
        // �p�� CameraRig �M Anchor �������Z��
        float distance = Vector3.Distance(cameraRig.transform.position, transform.position + transform.forward * -0.05f);
        // �p��q Anchor �� CameraRig ����V
        Vector3 directionToCamera = (cameraRig.transform.position - transform.position).normalized;

        //Debug.LogError(distance);

        // �p�G�Z���p�� 0.5�AĲ�o�ƥ�
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
