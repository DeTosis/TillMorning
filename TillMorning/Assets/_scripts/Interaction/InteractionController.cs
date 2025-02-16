using Assets._scripts.Interaction;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterHandData))]
public class InteractionController : MonoBehaviour {
    Camera camera => GetComponentInChildren<Camera>();
    CharacterHandData characterHandData => GetComponent<CharacterHandData>();

    [Header("Debug")]
    [SerializeField] PopupData[] popupData;
    [SerializeField] GameObject activePopup;
    [SerializeField] GameObject hittedObject;
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] float raycastLength = 10f;
    IInteractable interactableObject;

    private void Update() {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastLength, raycastLayerMask)) {
            hittedObject = hit.transform.gameObject;
            hittedObject.TryGetComponent<IInteractable>(out interactableObject);

            if (activePopup == null && interactableObject != null) {
                GameObject prefab = GetPrefab(interactableObject.InteractionType);
                if (prefab == null) throw new ArgumentNullException("Prefab for this type of action is undefined");
                CreatePopup(prefab);
            }

        } else {
            hittedObject = null;
            interactableObject = null;
        }

        if (activePopup != null && interactableObject == null) {
            Destroy(activePopup);
            activePopup = null;
        }
    }

    GameObject GetPrefab(ObjectInteractionType.InteractionType type) {
        foreach (var i in popupData) {
            if (i.interactType == type) 
                return i.popupPrefab;
        }
        return null;
    }

    void CreatePopup(GameObject popup) {
        activePopup = Instantiate(popup);
        activePopup.transform.position = hittedObject.transform.position;
        activePopup.transform.parent = hittedObject.transform;

        PlayerFollowing followingPopupData;
        activePopup.TryGetComponent<PlayerFollowing>(out followingPopupData);
        followingPopupData.SetTarget(transform);
    }

    public void InteractWithObject() {
        if (interactableObject == null) return;
        if (interactableObject.InteractionType == ObjectInteractionType.InteractionType.Pickup && characterHandData.isHandBusy) return;
        
        interactableObject.Interact(transform);
    }
}
[Serializable]
public class PopupData {
    [SerializeField] public GameObject popupPrefab;
    [SerializeField] public ObjectInteractionType.InteractionType interactType;
}
