using UnityEngine;

public class LoadAnchors : MonoBehaviour
{
    public GameObject anchorPrefab; // 用來作為標記點的物件

    private void Start()
    {
        LoadAnchorsFromPlayerPrefs();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            // 刪除所有 PlayerPrefs
            PlayerPrefs.DeleteAll();
            // 刪除所有場上的錨點
            ClearAllAnchors();
        }
    }

    // 加載所有儲存的錨點
    public void LoadAnchorsFromPlayerPrefs()
    {
        int anchorCount = PlayerPrefs.GetInt("AnchorCount", 0);  // 取得儲存的錨點數量
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
            Instantiate(anchorPrefab, anchorPosition, Quaternion.identity);  // 在儲存的位置生成錨點
        }
    }

    // 刪除場景中所有錨點
    private void ClearAllAnchors()
    {
        // 找到場景中所有帶有錨點標籤的物件
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
