using UnityEngine;

public class PlaceAnchor : MonoBehaviour
{
    public GameObject anchorPrefab; // 用來作為標記點的物件
    public Transform controller; // 控制器的位置

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Vector3 anchorPosition = controller.position + controller.forward * 0.1f; // 控制器前方 0.1 米
            PlaceAnchorAt(anchorPosition);
        }
    }

    private void PlaceAnchorAt(Vector3 position)
    {
        // 生成錨點實例
        GameObject anchorInstance = Instantiate(anchorPrefab, position, Quaternion.identity);

        // 保存錨點位置
        SaveAnchorPosition(position);  // 儲存位置
    }

    // 儲存錨點的位置到 PlayerPrefs
    private void SaveAnchorPosition(Vector3 position)
    {
        int anchorCount = PlayerPrefs.GetInt("AnchorCount", 0); // 取得目前的錨點數量
        string keyX = "AnchorPosition_" + anchorCount + "_x";
        string keyY = "AnchorPosition_" + anchorCount + "_y";
        string keyZ = "AnchorPosition_" + anchorCount + "_z";

        // 儲存每個位置的 x, y, z 值
        PlayerPrefs.SetFloat(keyX, position.x);
        PlayerPrefs.SetFloat(keyY, position.y);
        PlayerPrefs.SetFloat(keyZ, position.z);
        PlayerPrefs.Save();

        // 增加錨點計數
        PlayerPrefs.SetInt("AnchorCount", anchorCount + 1);
        PlayerPrefs.Save();
    }
}
