// using UnityEngine;
// using Oculus.Interaction; // 需要確保 Oculus Integration 插件已安裝

// public class PosterDetection : MonoBehaviour
// {
//     public string targetLabel = "牆面裝飾"; // 牆面裝飾標籤名稱
//     public float triggerDistance = 1.0f; // 距離海報的觸發範圍
//     public GameObject highlightCube; // 用於顯示效果的物件（例如變色框）

//     private Transform _playerCamera;

//     void Start()
//     {
//         _playerCamera = Camera.main.transform; // 設置玩家的相機作為參考
//     }

//     void Update()
//     {
//         // 嘗試獲取當前掃描的場景物件
//         var currentRoom = MRUK.Instance?.GetCurrentRoom();
//         if (currentRoom == null) return;

//         // 遍歷場景中的所有標籤物件
//         foreach (var sceneObject in currentRoom.SceneObjects)
//         {
//             if (sceneObject.Label == targetLabel)
//             {
//                 // 計算玩家與場景物件的距離
//                 float distance = Vector3.Distance(sceneObject.transform.position, _playerCamera.position);

//                 // 如果在觸發範圍內，啟動效果
//                 if (distance <= triggerDistance)
//                 {
//                     TriggerEffect(sceneObject);
//                 }
//                 else
//                 {
//                     ResetEffect(sceneObject);
//                 }
//             }
//         }
//     }

//     void TriggerEffect(MRSceneObject sceneObject)
//     {
//         Renderer renderer = sceneObject.GetComponent<Renderer>();
//         if (renderer != null)
//         {
//             renderer.material.color = Color.green; // 將海報平面變色
//         }

//         if (highlightCube != null)
//         {
//             highlightCube.transform.localScale = sceneObject.VolumeBounds.Value.size; // 匹配大小
//             highlightCube.transform.position = sceneObject.transform.position; // 匹配位置
//             highlightCube.transform.rotation = sceneObject.transform.rotation; // 匹配旋轉
//             highlightCube.SetActive(true);
//         }

//         Debug.Log($"Triggered: {sceneObject.name}");
//     }

//     void ResetEffect(MRSceneObject sceneObject)
//     {
//         Renderer renderer = sceneObject.GetComponent<Renderer>();
//         if (renderer != null)
//         {
//             renderer.material.color = Color.white; // 恢復原始顏色
//         }

//         if (highlightCube != null)
//         {
//             highlightCube.SetActive(false);
//         }

//         Debug.Log($"Reset: {sceneObject.name}");
//     }
// }
