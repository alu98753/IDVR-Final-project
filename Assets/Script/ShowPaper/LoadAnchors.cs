using UnityEngine;

public class LoadAnchors : MonoBehaviour
{
    public GameObject anchorPrefab; // �Ψӧ@���аO�I������

    private void Start()
    {
        LoadAnchorsFromPlayerPrefs();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            // �R���Ҧ� PlayerPrefs
            PlayerPrefs.DeleteAll();
            // �R���Ҧ����W�����I
            ClearAllAnchors();
        }
    }

    // �[���Ҧ��x�s�����I
    public void LoadAnchorsFromPlayerPrefs()
    {
        int anchorCount = PlayerPrefs.GetInt("AnchorCount", 0);  // ���o�x�s�����I�ƶq
        //Debug.LogError("Total Anchors: " + anchorCount);

        for (int i = 0; i < anchorCount; i++)
        {
            string keyX = "AnchorPosition_" + i + "_x";
            string keyY = "AnchorPosition_" + i + "_y";
            string keyZ = "AnchorPosition_" + i + "_z";

            float x = PlayerPrefs.GetFloat(keyX);
            float y = PlayerPrefs.GetFloat(keyY);
            float z = PlayerPrefs.GetFloat(keyZ);

            Vector3 anchorPosition = new Vector3(x, y, z);
            Instantiate(anchorPrefab, anchorPosition, Quaternion.identity);  // �b�x�s����m�ͦ����I
        }
    }

    // �R���������Ҧ����I
    private void ClearAllAnchors()
    {
        // ���������Ҧ��a�����I���Ҫ�����
        GameObject[] anchors = GameObject.FindGameObjectsWithTag("Anchor");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject anchor in anchors)
        {
            Destroy(anchor);
        }
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
    }
}
