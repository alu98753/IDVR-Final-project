using UnityEngine;

public class PlaceAnchor : MonoBehaviour
{
    public GameObject anchorPrefab; // �Ψӧ@���аO�I������
    public Transform controller; // �������m

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Vector3 anchorPosition = controller.position + controller.forward * 0.1f; // ����e�� 0.1 ��
            PlaceAnchorAt(anchorPosition);
        }
    }

    private void PlaceAnchorAt(Vector3 position)
    {
        // �ͦ����I���
        GameObject anchorInstance = Instantiate(anchorPrefab, position, Quaternion.identity);

        // �O�s���I��m
        SaveAnchorPosition(position);  // �x�s��m
    }

    // �x�s���I����m�� PlayerPrefs
    private void SaveAnchorPosition(Vector3 position)
    {
        int anchorCount = PlayerPrefs.GetInt("AnchorCount", 0); // ���o�ثe�����I�ƶq
        string keyX = "AnchorPosition_" + anchorCount + "_x";
        string keyY = "AnchorPosition_" + anchorCount + "_y";
        string keyZ = "AnchorPosition_" + anchorCount + "_z";

        // �x�s�C�Ӧ�m�� x, y, z ��
        PlayerPrefs.SetFloat(keyX, position.x);
        PlayerPrefs.SetFloat(keyY, position.y);
        PlayerPrefs.SetFloat(keyZ, position.z);
        PlayerPrefs.Save();

        // �W�[���I�p��
        PlayerPrefs.SetInt("AnchorCount", anchorCount + 1);
        PlayerPrefs.Save();
    }
}
