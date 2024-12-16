using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GrabEventHandler : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable; // �ؼХi�������
    public GameObject Paper;
    private InteractableState last, now;
    private Collider[] colliders; // �Ω��˴��I������
    private BagManager BM;

    private void Awake()
    {
        BM = FindFirstObjectByType<BagManager>();
    }

    private void Update()
    {
        last = now;
        now = handGrabInteractable.State;

        // �ˬd�O�_�q������A����
        if (last == InteractableState.Select && (now == InteractableState.Normal || now == InteractableState.Hover))
        {
            CheckCollisionWithBag();
        }
    }

    private void CheckCollisionWithBag()
    {
        // ������� Collider
        Collider objectCollider = Paper.GetComponent<Collider>();
        if (objectCollider == null)
        {
            Debug.LogError("Interactable does not have a Collider!");
            return;
        }
        
        // �˴��P��L���󪺸I��
        colliders = Physics.OverlapSphere(objectCollider.bounds.center, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("bag")) // �p�G�I����a�� "bag" ���Ҫ�����
            {
                Debug.LogError("put in bag");
                BM.PaperInBag = 2;//���_��
                Destroy(Paper);
                return;
            }
        }
    }
}
