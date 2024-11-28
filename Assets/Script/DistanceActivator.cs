using UnityEngine;

public class DistanceActivator : MonoBehaviour
{
    public Transform user; // 用户的摄像头位置
    public Transform posterAnchor; // 海报的锚点位置
    public float activationDistance = 1.0f; // 激活距离（1米）
    private bool hasActivated = false;

    void Update()
    {
        float distance = Vector3.Distance(user.position, posterAnchor.position);
        if (distance <= activationDistance && !hasActivated)
        {
            Activate();
            hasActivated = true;
        }
        else if (distance > activationDistance && hasActivated)
        {
            Deactivate();
            hasActivated = false;
        }
    }

    void Activate()
    {
        // 在这里添加激活时的逻辑
        // Debug.Log("已进入激活范围！");
    }

    void Deactivate()
    {
        // 在这里添加取消激活时的逻辑
        // Debug.Log("已离开激活范围！");
    }
}
