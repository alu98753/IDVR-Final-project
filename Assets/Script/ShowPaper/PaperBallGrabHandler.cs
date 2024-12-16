using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;
using UnityEngine;

public class PaperBallGrabHandler : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable; // �ؼХi�������
    private InteractableState last, now;
    private Collider[] colliders; // �Ω��˴��I������
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

        // �ˬd�O�_�q������A����
        if (last == InteractableState.Select && (now == InteractableState.Normal || now == InteractableState.Hover))
        {
            CheckCollisionWithBag();
        }
    }

    private void CheckCollisionWithBag()
    {
        // ������� Collider
        Collider objectCollider = this.gameObject.GetComponent<Collider>();
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
                Destroy(this.gameObject);
                PPM.GetComponent<PaperPageManager>().skippedPages.Remove(int.Parse(this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text));
                PH = GameObject.FindWithTag("Paper").GetComponent<PaperHover>();
                //Debug.LogError(PPM.GetComponent<PaperPageManager>().skippedPages.Count + " " + PH.innerMaterial.Count);
                if (PH.it == -1 && PPM.GetComponent<PaperPageManager>().skippedPages.Count != PH.innerMaterial.Count)//�q�S���ܦ�(��paperball���^)
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
