using UnityEngine;

namespace Assets._scripts.Interaction {
    public interface IInteractable {
        public ObjectInteractionType.InteractionType InteractionType { get; set; }
        public void Interact(Transform interactor);
    }
}
