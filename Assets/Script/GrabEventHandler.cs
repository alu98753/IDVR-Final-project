using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GrabEventHandler : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable; // 目標可抓取物件
    public GameObject Paper;
    private InteractableState last, now;
    private Collider[] colliders; // 用於檢測碰撞物件
    private BagManager BM;

    private void Awake()
    {
        BM = FindFirstObjectByType<BagManager>();
    }

    private void Update()
    {
        last = now;
        now = handGrabInteractable.State;

        // 檢查是否從抓取狀態釋放
        if (last == InteractableState.Select && (now == InteractableState.Normal || now == InteractableState.Hover))
        {
            CheckCollisionWithBag();
        }
    }

    private void CheckCollisionWithBag()
    {
        // 獲取物件的 Collider
        Collider objectCollider = Paper.GetComponent<Collider>();
        if (objectCollider == null)
        {
            Debug.LogError("Interactable does not have a Collider!");
            return;
        }
        
        // 檢測與其他物件的碰撞
        colliders = Physics.OverlapSphere(objectCollider.bounds.center, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("bag")) // 如果碰撞到帶有 "bag" 標籤的物件
            {
                Debug.LogError("put in bag");
                BM.PaperInBag = 2;//收起來
                Destroy(Paper);
                return;
            }
        }
    }
}
