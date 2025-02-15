using Assets._scripts.Interaction;
using UnityEngine;

[RequireComponent(typeof(CharacterHandData))]
public class InteractionController : MonoBehaviour {
    Camera camera => GetComponentInChildren<Camera>();
    CharacterHandData characterHandData => GetComponent<CharacterHandData>();

    [Header("Debug")]
    [SerializeField] GameObject interactionPopup;
    [SerializeField] GameObject activePopup;
    [SerializeField] GameObject hittedObject;
    [SerializeField] LayerMask raycastLayerMask;
    IInteractable interactableObject;

    private void Update() {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask)) {
            hittedObject = hit.transform.gameObject;
            hittedObject.TryGetComponent<IInteractable>(out interactableObject);

            if (activePopup == null && interactableObject != null) {
                CreatePickupPopup();
            }

        } else {
            hittedObject = null;
            interactableObject = null;
        }
        if (activePopup != null && interactableObject == null) {
            Destroy(activePopup);
            activePopup = null;
        }

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }

    void CreatePickupPopup() {
        activePopup = Instantiate(interactionPopup);
        activePopup.transform.position = hittedObject.transform.position;
        activePopup.transform.parent = hittedObject.transform;

        PopupPlayerFollowing followingPopupData;
        activePopup.TryGetComponent<PopupPlayerFollowing>(out followingPopupData);
        followingPopupData.SetTarget(transform);
    }

    public void InteractWithObject() {
        if (interactableObject == null || characterHandData.isHandBusy) return;
        interactableObject.Interact(transform);
    }
}
