using Assets._scripts.Interaction;
using UnityEngine;

public class Interaction_AddToFire_ : MonoBehaviour, IInteractable {
    [SerializeField] FireIntensityController fireController;
    [field: SerializeField] public ObjectInteractionType.InteractionType InteractionType { get; set; }

    public void Interact(Transform interactor) {
        CharacterHandData characterHandData;
        if (!interactor.gameObject.TryGetComponent<CharacterHandData>(out characterHandData)) return;
        if (characterHandData.objectInHand == null) return;

        BurnableStat burnableStat;
        if (characterHandData.objectInHand.TryGetComponent<BurnableStat>(out burnableStat)) {
            if (fireController.AddFuel(burnableStat.fuelAmmount)) {
                Destroy(characterHandData.objectInHand);
            }
        }
    }
}
