using UnityEngine;

public class PlayerFollowing : MonoBehaviour {
    [SerializeField] Transform objectToFollow;

    public void SetTarget(Transform target) {
        objectToFollow = target;
    }

    private void FixedUpdate() {
        transform.LookAt(objectToFollow.position);
    }
}
