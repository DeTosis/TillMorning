using UnityEngine;

public class CharacterHandData : MonoBehaviour {
    [field:SerializeField] public GameObject objectInHand { get; private set; }
    public bool isHandBusy => objectInHand != null;

    public void SetObjectInHand(GameObject obj) {
        objectInHand = obj;
    }
}
