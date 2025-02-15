using UnityEngine;

public class CharacterHandData : MonoBehaviour {
    [SerializeField] GameObject objectInHand;
    public bool isHandBusy => objectInHand != null;

    public void SetObjectInHand(GameObject obj) {
        objectInHand = obj;
    }
}
