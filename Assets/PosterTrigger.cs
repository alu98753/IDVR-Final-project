using UnityEngine;

public class PosterTrigger : MonoBehaviour
{
    public Transform player; // 玩家位置 (一般為 XR Camera)
    public float triggerDistance = 1.0f; // 觸發距離

    private bool hasTriggered = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < triggerDistance && !hasTriggered)
        {
            hasTriggered = true;
            TriggerEvent();
        }
    }

    void TriggerEvent()
    {
        Debug.Log("Player is near the poster!");
        // 添加你的觸發行為 (例如顯示互動介面或播放動畫)
    }
}
