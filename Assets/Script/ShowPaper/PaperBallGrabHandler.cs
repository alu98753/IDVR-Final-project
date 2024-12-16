using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;
using UnityEngine;

public class PaperBallGrabHandler : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable; // 目標可抓取物件
    private InteractableState last, now;
    private Collider[] colliders; // 用於檢測碰撞物件
    private GameObject PPM;
    public PaperHover PH;

    private void Awake()
    {
        PPM = GameObject.Find("PaperPageManager");
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
        Collider objectCollider = this.gameObject.GetComponent<Collider>();
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
                Destroy(this.gameObject);
                PPM.GetComponent<PaperPageManager>().skippedPages.Remove(int.Parse(this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text));
                PH = GameObject.FindWithTag("Paper").GetComponent<PaperHover>();
                //Debug.LogError(PPM.GetComponent<PaperPageManager>().skippedPages.Count + " " + PH.innerMaterial.Count);
                if (PH.it == -1 && PPM.GetComponent<PaperPageManager>().skippedPages.Count != PH.innerMaterial.Count)//從沒有變有(有paperball收回)
                {
                    PH.it = int.Parse(this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
                    PH.inner.GetComponent<Renderer>().material = PH.innerMaterial[PH.it];
                    Debug.LogError(PH.it);
                }
                return;
            }
        }
    }
}
