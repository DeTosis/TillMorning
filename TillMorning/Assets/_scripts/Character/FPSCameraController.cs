using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class FPSCameraController : MonoBehaviour {
    MovementController movementController => GetComponent<MovementController>();

    [Header("Camera Settings")]
    [SerializeField] Camera camera;
    [SerializeField] float sensetivityX;
    [SerializeField] float sensetivityY;
    [SerializeField] float angleClamp = 60;

    [Header("Sine Equasion Properties")]
    [SerializeField] float sineShakeIntensity = 6f;
    [SerializeField] float cameraShakeClamp_Y;
    [SerializeField] float sineEquPow = 2;

    float xRotation;
    float cameraStartupPositionY;

    float mouseX;
    float mouseY;
    
    private void Awake() {
        cameraStartupPositionY = camera.transform.localPosition.y;
    }
    
    public void MouseAxisInput(Vector2 mouseInput) {
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
    }

    float cameraShakeTimer = 0f;
    private void Update() {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime * sensetivityX);
        xRotation -= mouseY * Time.deltaTime * sensetivityX;
        xRotation = Mathf.Clamp(xRotation, -angleClamp, angleClamp);

        Vector3 rotation = transform.eulerAngles;
        rotation.x = xRotation;

        camera.transform.eulerAngles = rotation;

        //Sine camera Shake
        if (!movementController.isMoving) {
            Vector3 returnmentVector = camera.transform.localPosition;

            returnmentVector.y = camera.transform.localPosition.y < cameraStartupPositionY ?
                returnmentVector.y = cameraStartupPositionY - camera.transform.localPosition.y * Mathf.Pow(Time.deltaTime, 2) :
                returnmentVector.y = camera.transform.localPosition.y - cameraStartupPositionY * Mathf.Pow(Time.deltaTime, 2);

            camera.transform.localPosition = returnmentVector;
            return;
        }

        if (cameraShakeTimer < Mathf.PI) {
            cameraShakeTimer += Time.deltaTime;

            float sin = Mathf.Sin(cameraShakeTimer * sineShakeIntensity);
            float powdSin = Mathf.Pow(sin, sineEquPow);
            camera.transform.localPosition = new Vector3(0, Mathf.Lerp(
                cameraStartupPositionY - cameraShakeClamp_Y, cameraStartupPositionY + cameraShakeClamp_Y, powdSin), 0);
        } else {
            cameraShakeTimer = 0f;
        }
    }
}
