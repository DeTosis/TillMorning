using Assets._scripts.Interaction;
using UnityEngine;

public class Interaction_AddToFire_ : MonoBehaviour, IInteractable {
    [field: SerializeField] public ObjectInteractionType.InteractionType InteractionType { get; set; }

    public void Interact(Transform interactor) {
        CharacterHandData characterHandData;
        interactor.gameObject.TryGetComponent<CharacterHandData>(out characterHandData);
        Destroy(characterHandData.objectInHand);
    }
}
