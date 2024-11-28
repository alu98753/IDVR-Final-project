using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;

public class SceneObjectDebugger : MonoBehaviour
{
    void Start()
    {
        PrintAllAnchors();
    }

    void PrintAllAnchors()
    {
        // 获取当前房间
        MRUKRoom rooms = MRUK.Instance.GetCurrentRoom();

       // 打印每个房间的名称
        Debug.LogWarning($"Number of rooms: {rooms}");
        // Debug.LogWarning($"Room Name: {rooms.name}");


        if (rooms == null)
        {

            // Debug.LogWarning($"{a}No room data found. Ensure the scene is loaded and space mapping is active.");
            return;
        }

        // 遍历当前房间中的所有锚点（anchors）
        Debug.Log($"Number of anchors in the room: {rooms.Anchors.Count}");
        foreach (var anchor in rooms.Anchors)
        {
            Debug.Log($"Anchor Name: {anchor.name}, Label: {anchor.Label}");
        }
    }
}
