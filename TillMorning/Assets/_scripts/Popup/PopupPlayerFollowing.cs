using UnityEngine;

public class PopupPlayerFollowing : MonoBehaviour {
    [SerializeField] Transform objectToFollow;

    public void SetTarget(Transform target) {
        objectToFollow = target;
    }

    private void FixedUpdate() {
        transform.LookAt(objectToFollow.position);
    }
}
