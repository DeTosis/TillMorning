using Assets._scripts.Interaction;
using UnityEngine;

public class Interaction_PickUp_ : MonoBehaviour, IInteractable {
    [SerializeField] Vector3 objectInHandOffset;
    [SerializeField] Vector3 objectInHandRotation;
    [SerializeField] GameObject objectInHand;
    [field:SerializeField] public ObjectInteractionType.InteractionType InteractionType { get; set; }

    public void Interact(Transform interactor) {
        CharacterHandData characterHandData;
        if (!interactor.TryGetComponent<CharacterHandData>(out characterHandData)) return;
        GameObject newObj = Instantiate(objectInHand);

        newObj.transform.position = interactor.position;
        newObj.transform.parent = interactor;

        newObj.transform.localPosition = objectInHandOffset;
        newObj.transform.localEulerAngles = objectInHandRotation;

        characterHandData.SetObjectInHand(newObj);
        Destroy(gameObject);
    }
}
