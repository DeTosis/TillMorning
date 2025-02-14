using UnityEngine;

public class MouseLockSettings : MonoBehaviour {
    [SerializeField] bool isCursorVisible = false;
    [SerializeField] bool isCursorLocked = true;

    private void FixedUpdate() {
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = isCursorVisible;
    }
}
